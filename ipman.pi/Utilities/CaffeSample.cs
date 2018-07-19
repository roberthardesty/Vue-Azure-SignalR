﻿using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ipman.pi.Utilities
{
    class CaffeSample
    {
        public void Run()
        {
            const string protoTxt = @"Data/bvlc_googlenet.prototxt";
            const string caffeModel = "bvlc_googlenet.caffemodel";
            const string synsetWords = @"Data/synset_words.txt";
            var classNames = File.ReadAllLines(synsetWords)
                .Select(line => line.Split(' ').Last())
                .ToArray();

            Console.Write("Downloading Caffe Model...");
            PrepareModel(caffeModel);
            Console.WriteLine(" Done");

            using (var net = CvDnn.ReadNetFromCaffe(protoTxt, caffeModel))
            using (var img = new Mat(@"Data/Birds.jpg"))
            {
                Console.WriteLine("Layer names: {0}", string.Join(", ", net.GetLayerNames()));
                Console.WriteLine();

                // Convert Mat to batch of images
                using (var inputBlob = CvDnn.BlobFromImage(img, 1, new Size(224, 224), new Scalar(104, 117, 123)))
                {
                    net.SetInput(inputBlob, "data");
                    using (var prob = net.Forward("prob"))
                    {
                        // find the best class
                        //GetMaxClass(prob, out int classId, out double classProb);
                        // int classId;
                        double classProb;
                        //Console.WriteLine("Best class: #{0} '{1}'", classId, classNames[classId]);
                        //Console.WriteLine("Probability: {0:P2}", classProb);

                        using (Mat probMat = prob.Reshape(1, 1))
                        {
                            Cv2.MinMaxLoc(probMat, out _, out classProb, out _, out var classNumber);
                            int classId = classNumber.X;
                            Console.WriteLine("Best class: #{0} '{1}'", classId, classNames[classId]);
                            Console.WriteLine("Probability: {0:P2}", classProb);

                            Console.WriteLine("Columns Please: {0}", probMat.Cols);
                            Console.WriteLine("1. For the snowbird: {0}", probMat.Get<double>(13, 0));
                            Console.WriteLine("2. For the snowbird: {0:P2}", probMat.Get<double>(0, 13));

                            for (int i = 0; i < probMat.GetArray(0, 13).Length; i++)
                            {
                                Console.WriteLine("Here we go: {0:P2}", probMat.GetArray(0, 13)[i]);
                            }

                            Console.WriteLine("3. For the snowbird: {0}", probMat.GetArray(0, 13)[0]);
                            for (int i = 0; i < probMat.Cols; i++)
                            {
                                classId = i;
                                classProb = probMat.At<double>(0, i);
                                double probInt = probMat.At<double>(i, 0);
                                Console.WriteLine("Close classes: #{0} '{1}'", classId, classNames[classId]);
                                Console.WriteLine("Prob Int: {0}", probInt);
                                Console.WriteLine("Probability: {0} \n", classProb);
                            }

                        }

                    }
                }
            }
        }

        private static byte[] DownloadBytes(string url)
        {
            var client = WebRequest.CreateHttp(url);
            using (var response = client.GetResponse())
            using (var responseStream = response.GetResponseStream())
            {
                using (var memory = new MemoryStream())
                {
                    responseStream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
        }

        private static void PrepareModel(string fileName)
        {
            if (!File.Exists(fileName))
            {
                var contents = DownloadBytes("http://dl.caffe.berkeleyvision.org/bvlc_googlenet.caffemodel");
                File.WriteAllBytes(fileName, contents);
            }
        }

        /// <summary>
        /// Find best class for the blob (i. e. class with maximal probability)
        /// </summary>
        /// <param name="probBlob"></param>
        /// <param name="classId"></param>
        /// <param name="classProb"></param>
        private static void GetMaxClass(Mat probBlob, out int classId, out double classProb)
        {
            // reshape the blob to 1x1000 matrix
            using (Mat probMat = probBlob.Reshape(1, 1))
            {
                Cv2.MinMaxLoc(probMat, out _, out classProb, out _, out var classNumber);
                classId = classNumber.X;
            }
        }
    }
}

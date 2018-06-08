const path = require('path');
const webpack = require('webpack');
const utils = require('./ClientApp/buildSettings/utils')
const config = require('./ClientApp/config')
const vueLoaderConfig = require('./ClientApp/buildSettings/vue-loader.conf')
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [{
        stats: { modules: false },
        entry: { 'main': './ClientApp/boot.ts' },
        resolve: {
            extensions: ['.js', '.vue', '.json', '.ts'],
            alias: {
                'vue$': 'vue/dist/vue',
                'components': path.resolve(__dirname, './ClientApp/components'),
                'views': path.resolve(__dirname, './ClientApp/views'),
                'utils': path.resolve(__dirname, './ClientApp/build/utils'),
                'api': path.resolve(__dirname, './ClientApp/store/api'),
                '@': path.resolve('ClientApp')
            }
        },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: '/dist/'
        },
        module: {
            rules: [
                {
                    test: /\.ts$/,
                    exclude: /node_modules|vue\/ClientApp/,
                    include: /ClientApp/,
                    loader: 'ts-loader',
                    options: {
                        appendTsSuffixTo: [/\.vue$/]
                    }
                    //use: 'awesome-typescript-loader?silent=true'
                  },
                  {
                    test: /\.vue$/,
                    include: /ClientApp/,
                    loader: 'vue-loader',
                    options: vueLoaderConfig
                  },
                  {
                    test: /\.js$/,
                    loader: 'babel-loader',
                    include: /ClientApp/
                  },
                  {
                    test: /\.(png|jpe?g|gif|svg)(\?.*)?$/,
                    loader: 'url-loader',
                    options: {
                      limit: 10000,
                      name: utils.assetsPath('img/[name].[hash:7].[ext]')
                    }
                  },
                  {
                    test: /\.(mp4|webm|ogg|mp3|wav|flac|aac)(\?.*)?$/,
                    loader: 'url-loader',
                    options: {
                      limit: 10000,
                      name: utils.assetsPath('media/[name].[hash:7].[ext]')
                    }
                  },
                  {
                    test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/,
                    loader: 'url-loader',
                    options: {
                      limit: 10000,
                      name: utils.assetsPath('fonts/[name].[hash:7].[ext]')
                    }
                  },
                  ...utils.styleLoaders({ sourceMap: config.dev.cssSourceMap })
            ]
        },
        plugins: [
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            })
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
        ] : [
                // Plugins that apply in production builds only
                new UglifyJsPlugin(),
                new webpack.DefinePlugin({
                    'process.env': {
                        'NODE_ENV': JSON.stringify("production")
                    }
                    }),
                new ExtractTextPlugin('site.css')
            ])
    }];
};

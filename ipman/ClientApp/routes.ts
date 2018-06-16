import CounterExample from 'components/counter-example.vue'
import HomePage from 'components/home-page.vue'
import DashBoardPage from 'components/dashboard-page.vue';

export const routes = [
    { path: '/', component: HomePage, display: 'Home', style: 'glyphicon glyphicon-home' },
    { path: '/counter', component: CounterExample, display: 'Counter', style: 'glyphicon glyphicon-education' },
    { path: '/dashboard', component: DashBoardPage, display: 'Dashboard', style: 'home'}
]
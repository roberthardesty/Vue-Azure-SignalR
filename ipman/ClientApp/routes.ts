import SitePage from 'components/site-page.vue'
import HomePage from 'components/home-page.vue'
import DashBoardPage from 'components/dashboard-page.vue';

export const routes = [
    { path: '/', component: HomePage, display: 'Home', style: 'home' },
    { path: '/sites/:site', component: SitePage, display: 'Sites', style: 'phone' },
    { path: '/dashboard', component: DashBoardPage, display: 'Dashboard', style: 'person'}
]
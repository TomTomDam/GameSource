﻿//import Vue from 'vue';
//import VueRouter from 'vue-router';
////BrowseGamesPage
//import BrowseGamesPage from '../views/Navbar/BrowseGamesPage.vue';
//import BrowseGamesByGenre from '../components/Navbar/BrowseGamesByGenre.vue';
//import BrowseGamesByPlatform from '../components/Navbar/BrowseGamesByPlatform.vue';

//Vue.use(VueRouter);

//export default new VueRouter({
//    mode: 'history',
//    base: process.env.BASE_URL,
//    routes: [
//        {
//            path: '/genre/:genreId',
//            name: 'genre',
//            component: BrowseGamesByGenre
//        },
//        {
//            path: '/platform/:platformId',
//            name: 'platform',
//            component: BrowseGamesByPlatform
//        },
//        {
//            path: '/component/',
//            name: 'component',
//            component: Component,
//            children: [
//                {
//                    path: '/child-component/:id',
//                    name: 'child-component',
//                    component: ChildComponent
//                }
//            ]
//        }
//    ]
//});
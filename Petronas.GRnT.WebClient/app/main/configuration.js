/*
 * @CreateTime: Dec 1, 2017 11:33 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 15, 2018 9:23 PM
 * @Description: Modify Here, Please 
 */

(function() {
    'use strict';

    angular
        .module('app')
        .constant('AppConfig', {
            Currency: 'MYR',
            GA: {
                tracker: 'UA-109807683-2',
                name: 'HT',
                domain: 'none'
            },
            Prod: {
                host: 'http://10.24.1.234',
                port: '8086'
            },
            Dev: {
                host: 'http://localhost',
                port: '8080'
            },
            Mock: {
                host: 'http://localhost',
                port: '8888'
            },
            Auth0: {
                host: 'http://101.78.16.77',
                port: '8081'
            },
            AuthToken: {
                TypeKey: 'Type',
                TokenKey: 'Token',
                ClientId: '8c6bb6d2-94fa-e711-9bd9-9cd21e367ec4'
                // ClientId: '8b6bb6d2-94fa-e711-9bd9-9cd21e367ec4'
            },
            Role: {
                admin: 'Admin',
                moderator: 'Moderator',
                pm: 'PM'
            }
        })
        .constant('PET_ROUTE', {
            ErrorAbstract: 'error',
            NotFound: 'error.404',
            DefaultAbstract: 'default',
            Landing: 'default.landing',
            Login: 'default.login',
            Register: 'default.register',
            RootAbstract: 'root',
            HomePage: 'root.home',
            Example: 'root.samples',
            AboutAbstract: 'about',
            AboutUs: 'about.aboutus',
            Samples: 'about.samples',
            ManagementAbstract: 'management',
            UserPage: 'management.user',
            PACListPage: 'management.pac'
        })
        .constant('API', {
            RightTopMenu: {
                Prod: '/api/MasterData/Navigation/GetTopRight',
                Dev: '/data/sampledata/action-topright.json',
                Mock: '/data/sampledata/action-topright.json'
            },
            LeftAside: {
                Prod: '/api/MasterData/Navigation/Get',
                Dev: '/data/sampledata/leftaside.json',
                Mock: '/data/sampledata/leftaside.json'
            },
            Organization: {
                Prod: '/api/Account/GetOrg',
                Dev: '/data/sampledata/organization.json',
                Mock: '/data/sampledata/organization.json'
            },
            SignIn: {
                Prod: '/token',
                Dev: '/data/sampledata/signin.json',
                Mock: '/data/sampledata/signin.json'
            },
            Login: {
                Prod: '/api/users/login',
                Dev: '/data/sampledata/signin.json',
                Mock: '/data/sampledata/signin.json'
            },
            Register: {
                Prod: '/api/users/login',
                Dev: '/data/sampledata/signin.json',
                Mock: '/data/sampledata/signin.json'
            },
            SortBy: {
                Prod: '',
                Dev: '/data/sampledata/sortby.json',
                Mock: '/data/sampledata/sortby.json'
            },
            Selector: {
                Prod: '',
                Dev: '/data/sampledata/multiple-selector.json',
                Mock: '/data/sampledata/multiple-selector.json'
            }
        })
        .value('AppValue', {
            AppTitle: 'Petronas Media Portal',
            RunMode: 'Dev',
            NotifyTimer: 3000
        });

}());
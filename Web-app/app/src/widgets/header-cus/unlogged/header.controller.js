/*
 * @CreateTime: Feb 1, 2018 12:00 PM
 * @Author: Thuan Nguyen
 * @Contact: thuannd15@fsoft.com.vn
 * @Last Modified By:
 * @Last Modified Time:
 * @Description:
 */
(function () {
    'use strict';

    angular
        .module('app')
        .controller('CusUnLgHeaderCtrl', CusUnLgHeaderCtrl);

    /** @ngInject */
    CusUnLgHeaderCtrl.$inject = ['$scope'];

    function CusUnLgHeaderCtrl($scope) {
        var vm = this;

        init();

        function init() {
            $('.has-submenu').click(function () {
                if ($(this).find('.material-icons').html() == 'keyboard_arrow_down') {
                    clearMenuState();
                    $('.has-submenu').removeClass('menu-selected');
                    $(this).addClass('menu-selected');
                    $($(this).attr('data-content')).show();
                    $(this).find('.material-icons').html('keyboard_arrow_up');
                } else {
                    $(this).removeClass('menu-selected');
                    $($(this).attr('data-content')).hide();
                    $(this).find('.material-icons').html('keyboard_arrow_down');
                }
            });

            $('.sub-menu-wrap a').click(function () {
                clearMenuState();
            });

            function clearMenuState() {
                $('.sub-menu-wrap').hide();
                $('.has-submenu').find('.material-icons').html('keyboard_arrow_down');                
            }
        }

    }

}());
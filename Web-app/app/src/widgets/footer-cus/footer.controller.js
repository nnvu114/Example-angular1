(function () {
    'use strict';

    angular
        .module('app')
        .constant('footerConstant', {});
})();
(function () {
    'use strict';

    angular
        .module('app')
        .controller('FooterController', FooterController);

    FooterController.$inject = ['footerConstant'];

    /* @ngInject */
    function FooterController(footerConstant) {
        var vm = this;
        vm.footerConstant = footerConstant;
        activate();

        function activate() {
            $('.input-line').each(function (e) {
                if (this.value || this.placeholder) {
                    $(this).prev().css({
                        top: '-16px',
                        'font-size': '14px'
                    })
                }
            })
            $('input').each(function (e) {
                if ($(this).is(':disabled') && $(this).prev().is('label')) {
                    $(this).prev().css({
                        color: 'rgba(0,0,0,.4)',
                    })
                }
                if ($(this).is(':disabled') && $(this).parent().hasClass('text-box-label-container')) {
                    $(this).parent().css({
                        'border-style': 'dashed',
                    })
                }
            })
        }

        $('.content-item').click(function () {
            $('html, body').animate({
                scrollTop: $($.attr(this, 'href')).offset().top
            }, 300);
        });

        $('.input-line').focus(function (e) {
            $(this).prev().css({
                top: '-16px',
                color: '#00A19C',
                'font-size': '14px'
            })
        });
        $('.input-line').blur(function (e) {
            $(this).prev().css({
                color: '',
            });
            if (!this.value && !this.placeholder) {
                $(this).prev().css({
                    top: '8px',
                    'font-size': '16px'
                })
            }
        });
        $('.text-box-label-container .text-box').focus(function (e) {
            $(this).prev().css({
                top: '7px',
                left: '7px',
                color: '#00A19C',
                'font-size': '14px'
            });
            $(this).parent().css({
                border: '2px solid #00A19C'
            });
        });
        $('.text-box-label-container .text-box').blur(function (e) {
            if (!this.value && !this.placeholder) {
                $(this).prev().css({
                    top: '32px',
                    color: '',
                    'font-size': '16px',
                    'transition-duration': ''
                });
            } else {
                $(this).prev().css({
                    top: '8px',
                    left: '',
                    color: '',
                    'transition-duration': '0s'
                });
            }
            $(this).parent().css({
                padding: '',
                border: '',
            });
        });
        $('.text-box-container .text-box').focus(function (e) {
            $(this).prev().css({
                color: '#00A19C'
            });
        });
        $('.text-box-container .text-box').blur(function (e) {
            $(this).prev().css({
                color: ''
            });
        });

        $('.change-selection-colour').change(function (e) {
            $(this).css({
                color: 'black'
            });
        })
    }
})();
/*
 * @CreateTime: Dec 1, 2017 11:32 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Dec 1, 2017 11:32 PM
 * @Description: Object.isObjectEmpty(yourObject)
 */

(function() {
    'use strict';

    Math.log10 = Math.log10 || function(arg) {
        return Math.log(arg) / Math.LN10 + 0.0001;
    };

    /*jshint freeze: false */
    Object.defineProperty(Object.prototype, 'isObjectEmpty', {
        value: function(obj) {
            if (Object.keys(obj).length === 0 && Object.getOwnPropertyNames(obj).length === 0) {
                return true;
            } else {
                return false;
            }
        }
    });
}());
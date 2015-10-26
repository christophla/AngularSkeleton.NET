//=============================================================================
// NOTICE:  ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS THE PROPERTY OF 
// AIDO INCORPORATED AND ITS SUPPLIERS, IF ANY. THE INTELLECTUAL AND TECHNICAL 
// CONCEPTS CONTAINED HEREIN ARE PROPRIETARY TO AIDO INCORPORATED AND ITS 
// SUPPLIERS AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENTS IN 
// PROCESS, AND ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW. DISSEMINATION 
// OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL IS STRICTLY FORBIDDEN 
// UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED FROM AIDO INCORPORATED.
//=============================================================================


// ****************************************************************************
// Interfaces ILogger
//

interface ILogger {
    info(message: string)
    success(message: string)
    warning(message: string)
    error(message: string, obj?: any)
    debug(message: string, obj?: any)
    dump(arr: any, level: number)
}


// ****************************************************************************
// Module app.services.logger
//

var m = angular.module('common.services.logger', [])
 
   
// ****************************************************************************
// Service 'logger' (Toastr)
//

m.factory('logger', ['settings', (settings: ISystemSettings) => {
    // This logger wraps the toastr logger and also logs to console
    // toastr.js is library by John Papa that shows messages in pop up toast.
    // https://github.com/CodeSeven/toastr
    toastr.options.timeOut = 3500; // 2 second toast timeout
    toastr.options.positionClass = 'toast-bottom-right';

    function info(message) {
        toastr.info(message);
        log("Info: " + message)
    }

    function success(message) {
        toastr.success(message);
        log("Success: " + message)
    }

    function warning(message) {
        toastr.warning(message);
        log("Warning: " + message)
    }

    function error(message, data?) {

        if (typeof data === "undefined") {
            alert(message)
            toastr.error(message)
            log(message)
        }
        else if (data.modelState) {
            var errorList = '<ul>'
            for (var key in data.modelState) {
                errorList += '<li>' + data.modelState[key][0] + '</li>'
            }
            errorList += '</ul>'
            toastr.error(errorList, message)
        }
        else if (data.error_description) {
            toastr.error(data.error_description, message)
            log(data.error_description, message)
        }
        else if (data.message) {
            toastr.error(data.message, message)
            log(data.message, message)
        }
        else if (data) {
            toastr.error(data)
            log(data)
        }
        else {
            toastr.error(message)
        }
    }

    function debug(message, obj?) {
        //toastr.info(message);
        if (settings.debugEnabled)
            log("Debug: " + message, JSON.stringify(obj))
    }

    // IE and google chrome workaround
    // http://code.google.com/p/chromium/issues/detail?id=48662
    function log(message, obj?) {
        var console = window.console
        if (obj) {
            if (console && console.log && console.log.apply && console.group) {
                console.group(message)
                console.log(obj)
                console.groupEnd()
            }
        } else {
            !!console && console.log(message)
        }
    }

    function dump(arr, level) {
        var dumpedText = ''
        if (!level)
            level = 0

        //The padding given at the beginning of the line.
        var levelPadding = ''
        for (var j = 0; j < level + 1; j++)
            levelPadding += '    '

        if (typeof (arr) == 'object') { //Array/Hashes/Objects
            for (var item in arr) {
                var value = arr[item]

                if (typeof (value) == 'object') { //If it is an array,
                    dumpedText += levelPadding + "'" + item + "' ...\n"
                    dumpedText += dump(value, level + 1)
                }
                else {
                    dumpedText += levelPadding + "'" + item + "' => \"" + value + "\"\n"
                }
            }
        }
        else { //Stings/Chars/Numbers etc.
            dumpedText = `===>${arr}<===(${typeof (arr)})`
        }
        return dumpedText
    }

    var logger: ILogger = {
        error: error,
        info: info,
        success: success,
        warning: warning,
        debug: debug,
        dump: dump
    }

    return logger
}]) 
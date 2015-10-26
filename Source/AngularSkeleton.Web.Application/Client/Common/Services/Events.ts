//=============================================================================
// Copyright (c) 2015 AIDO Incorporated 
// All Rights Reserved.
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
// Interfaces app.services.events 
//

interface IEvents {
    on(event: string, callback: any, unsubscribeOnResponse?: boolean): void;
    emit(event: string, extraParameters?: any);
}


// ****************************************************************************
// Module app.services.events
//

var m = angular.module('common.services.events', [])


// ****************************************************************************
// Service 'events' 
//

m.factory('events', () => {
    var el = document.createElement('div')

    var eventsListener: IEvents = {
        on(event, callback, unsubscribeOnResponse?) {
            $(el).on(event, function () {
                if (unsubscribeOnResponse) {
                    $(el).off(event)
                }
                callback.apply(this, arguments) //invoke client callback
            })
        },
        emit(event, extraParameters?) {
            $(el).trigger(event, extraParameters)
        }
    }
    return eventsListener
})  
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
// Underscore support
// 

angular.module('common.services.underscore', []).factory('_', () => {
    var win: any = window;
    return win._; // assumes underscore has already been loaded on the page
})

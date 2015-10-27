//=============================================================================
// Copyright (c) 2015 AIDO Incorporated 
// All Rights Reserved.
//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================


// ****************************************************************************
// Constants Module 
//

var m = angular.module('app.constants', []); 


// ****************************************************************************
// System settings 
//

interface IOption {
    key: string
    value: string
}

interface ISystemSettings { 
    apiBaseUri: string
    clientBaseUri: string
    currencyCodes: Array<IOption>
    currentTheme: string
    debugEnabled: boolean
    docsBaseUri: string
    moduleBaseUri: string
    themes: Array<string>
    widgetsBaseUri: string
}

m.constant('settings', <ISystemSettings> {
    apiBaseUri: window['skeleton_api_root'],
    clientBaseUri: window['skeleton_client_root'],
    currencyCodes: [
        { key: 'USD', value: 'United States Dollars' }
    ],
    currentTheme: 'frost',
    debugEnabled: true,
    docsBaseUri: '/api/rest/docs/index',
    moduleBaseUri: window['skeleton_client_root'] + '/app/modules',
    widgetsBaseUri: window['skeleton_client_root'] + '/app/widgets',
    themes: [
        'adminflare',
        'asphalt',
        'clean',
        'default',
        'dust',
        'frost',
        'purple-hills',
        'silver',
        'white'
    ]
})

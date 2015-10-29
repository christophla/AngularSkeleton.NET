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
// Core Application Module 
//

var m = angular.module('app', [ 
    'app.constants',
    'common.analytics',
    'common.filters',
    'common.forms',
    'common.security',
    'app.repositories',
    'app.services',
    'app.errors',
    'app.layout',
    'app.login',
    'app.catalog',
    'app.dashboard',
    'app.manage',
    'app.profile',
    'app.widgets',
    'angularMoment',
    'angular-ladda',
    'ngAnimate',
    'ngProgress',
    'ngSanitize',
    'restangular',
    'ui.bootstrap',
    'ui.router',
    'ui.select',
    'uiSwitch',
    'angular.morris-chart',
    'cgBusy'
]) 
 

// ****************************************************************************
// Configure 
//

m.config([
    '$httpProvider',
    '$locationProvider',
    '$stateProvider',
    '$urlRouterProvider',
    'laddaProvider',
    'RestangularProvider',
    'settings',
    (
        $httpProvider: ng.IHttpProvider,
        $locationProvider: ng.ILocationProvider,
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        laddaProvider: ladda.ILaddaProvider,
        restangularProvider: restangular.IProvider,
        settings: ISystemSettings
    ) => {

        $locationProvider.html5Mode(false)
        $urlRouterProvider.otherwise('/dashboard')
        laddaProvider.setOption({ style: 'expand-left' })
        restangularProvider.setBaseUrl(settings.apiBaseUri)
        
        // security 

        $stateProvider.state('app', {
            abstract: true, 
            resolve: {
                authorize: ['security',
                    (security: ISecurity) => security.authorization.authorize()
                ]
            },
            views: {
                'navbar@': {
                    templateUrl: `${settings.moduleBaseUri}/layout/navbar.tpl.html`,
                    controller: 'app.layout.navbar'
                },
                'sidebar@': {
                    templateUrl: `${settings.moduleBaseUri}/layout/sidebar.tpl.html`,
                    controller: 'app.layout.sidebar'
                },
                'footer@': {
                    templateUrl: `${settings.moduleBaseUri}/layout/footer.tpl.html`,
                    controller: 'app.layout.footer'
                }
            }
        })

    }
])


// ****************************************************************************
// Run 
//

m.run(['$rootScope', '$state', '$stateParams', '$http', 'security', 'services', 'ngProgressFactory',
    ($rootScope, $state, $stateParams, $http, security: ISecurity, services: IServices, progress: NgProgress.INgProgressProvider) => {
        
        $rootScope.progressbar = progress.createInstance()

        security.principal.configure({
            requestTokenName: 'Authorization',
            storageKey: 'skeleton.principal.identity',
            storageMode: PrincipalStorageMode.Session
        })
        
        $rootScope.$on('$stateChangeStart', (event, toState, toStateParams) => {
            
            $rootScope.progressbar.setHeight(4)
            $rootScope.toState = toState
            $rootScope.toStateParams = toStateParams
        
            if (security.principal.isIdentityResolved()) {
                security.authorization.authorize()
            }
        }) 

        $rootScope.$on('$stateChangeSuccess', (event, toState, toParams, fromState, fromParams) => {
            $rootScope.progressbar.complete()
        })

}]) 


// ****************************************************************************
// Controller app.container
//

m.controller('app.container', ['$scope', '$window', 'services', 'settings', ($scope, $window: ng.IWindowService, services: IServices, settings: ISystemSettings) => {
    services.logger.debug('Loading controller app.container.')
    
    $scope.minimized = false
    $scope.showcompact = false
    $scope.theme = `theme-${settings.currentTheme}`
    
    services.events.on('event:sidebar-toggle', () => {
        $scope.minimized = !$scope.minimized
        if ($window.screenX < 480) {
            $scope.showcompact = !$scope.showcompact
        }
    })
    
    services.events.on('event:update-theme', () => {
        services.logger.debug(`switching theme: ${settings.currentTheme}`)
        $scope.theme = `theme-${settings.currentTheme}`
    })
    
}]) 


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
// Module app.profile
//
 
var m = angular.module('app.profile', [])


// ****************************************************************************
// Configure app.catalog
//

m.config(['$urlRouterProvider', '$stateProvider', 'settings', ($urlRouterProvider: ng.ui.IUrlRouterProvider, $stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $stateProvider
        .state('app.profile', {
            url: '/profile',
            data: {
                roles: ['user']
            },
            controller: 'app.profile',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/profile/profile.tpl.html`,
                    controller: 'app.profile'
                }
            },
            resolve: {
                profile: ['services', (services: IServices) => services.profile.me()]
            }
        })
}])


// ****************************************************************************
// Controller app.profile
//

interface IProfileScope extends ng.IScope {
    currentTheme: string
    profile: IUser
    updateTheme(): void
    save(isValid: boolean): void
    submitted: boolean
    submitting: boolean
    themes: Array<string>
}

m.controller('app.profile', ['$scope', 'profile', 'repositories', 'services', 'settings',
    ($scope: IProfileScope, profile: IUser, repositories: IRepositories, services: IServices, settings: ISystemSettings) => {

        services.logger.debug('Loaded controller app.profile')

        $scope.currentTheme = settings.currentTheme
        $scope.profile = profile
        $scope.themes = settings.themes
        
        $scope.updateTheme = () => {
            services.logger.debug('switching theme')
            settings.currentTheme = profile.theme
            services.events.emit('event:update-theme')
        }
         
        $scope.save = (isValid: boolean) => {
            $scope.submitted = true

            if (isValid) {
                $scope.submitting = true
                repositories.users.save($scope.profile).then(() => {
                    services.logger.success('Updated profile')
                    $scope.updateTheme()
                    $scope.submitted = false
                    $scope.submitting = false
                }, response => {
                    services.logger.error('An error occurred updating the profile', response.data)
                    $scope.submitted = false
                    $scope.submitting = false
                })
            }
        }
    }
]) 
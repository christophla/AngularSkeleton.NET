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
// Module app.login
//

var m = angular.module('app.login', [])

// ****************************************************************************
// Configuration
//

m.config(['$stateProvider', 'settings',
    ($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $stateProvider
        .state('app.login', {
            url: '/login',
            data: {
                roles: []
            },
            views: {
                'fullscreen@': {
                    templateUrl: `${settings.moduleBaseUri}/login/login.tpl.html`,
                    controller: 'app.login'
                }
            }
        })
    }
]) 


// ****************************************************************************
// Controller app.login
//

m.controller('app.login', ['$scope', 'security', 'repositories', 'services', 'settings',
    ($scope, security: ISecurity, repositories: IRepositories, services: IServices, settings: ISystemSettings) => {

    services.logger.debug('Loading app.login controller.')

    $scope.auth = {}

    $scope.authenticate = (isValid: boolean) => {
        
        if (isValid) {

            $scope.submitted = true // spinner
             
            repositories.authentication.authenticate($scope.auth.username, $scope.auth.password).then((data: IAuthenticationToken) => {
                
                services.logger.success(`Logged in as: ${$scope.auth.username}`)
                services.logger.debug(`Using token: ${data.access_token}`)
                security.principal.authenticate({ name: $scope.auth.username, token: data.access_token, roles: ['user'] }) // base role

                // get roles and profile configuration

                services.profile.me().then((me: IUser) => {
                    var roles = (me.isAdmin) ? ['user', 'admin'] : ['user']
                    services.logger.debug(`Using roles: ${JSON.stringify(roles)}`)
                    security.principal.authenticate({ name: $scope.auth.username, token: data.access_token, roles: roles })
                    settings.currentTheme = me.theme
                    services.events.emit('event:update-theme')
                    services.state.go('app.dashboard', {})
                })

            }, response => {
                services.logger.error('An error occurred authenticating', response.data)
                $scope.submitted = false
                $scope.submitting = false
            })
        }
    }
}]) 
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
// Module app.dashboard
//
 
var m = angular.module('app.dashboard', [])
 

// ****************************************************************************
// Configure app.dashboard
//

m.config(['$stateProvider', 'settings', ($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $stateProvider
        .state('app.dashboard', {
            url: '/dashboard',
            data: {
                roles: ['user']
            },
            controller: 'app.dashboard',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/dashboard/dashboard.tpl.html`,
                    controller: 'app.dashboard'
                }
            },
            resolve: {
                profile: ['services', (services: IServices) => services.profile.me()]
            }
        })
}])


// ****************************************************************************
// Controller app.dashboard
//

interface IDashboardScope {
    fullName: string
}

m.controller('app.dashboard', ['$scope', 'profile', 'repositories', 'services',
    ($scope: IDashboardScope, profile: IUser, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.dashboard')
        
        $scope.fullName = profile.fullName()
    }
]) 
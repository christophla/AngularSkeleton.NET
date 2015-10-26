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

var m = angular.module('app.errors', [])

// ****************************************************************************
// Configuration
//

m.config(['$stateProvider', 'settings',
    ($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

        $stateProvider
            .state('app.accessdenied', {
                url: '/accessdenied',
                data: {
                    roles: []
                },
                views: {
                    'content@': {
                        templateUrl: `${settings.moduleBaseUri}/errors/accessdenied.tpl.html`,
                        controller: 'app.accessdenied'
                    }
                }
            })
    }
]) 


// ****************************************************************************
// Controller app.login
//

m.controller('app.accessdenied', ['$scope', 'security', 'repositories', 'services',
    ($scope, security: ISecurity, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loading app.accessdenied controller.')
        
    }]) 
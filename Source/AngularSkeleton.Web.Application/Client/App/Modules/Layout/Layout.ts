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
// Module app.layout
//

var m = angular.module('app.layout', [])


// ****************************************************************************
// Controller layout.navbar
//

m.controller('app.layout.navbar', ['$scope', '$state', 'security', 'services', ($scope: any, $state: ng.ui.IState, security: ISecurity, services: IServices) => {
    services.logger.debug('Loading app.layout.navbar controller')

    $scope.$state = $state
    
    security.principal.identity().then(data => {
        if(data) $scope.username = data.name
    })

    services.profile.me().then(data => {
        $scope.avataruri = data.avatar
    })

    $scope.toggleSidebar = () => {
        services.logger.debug('toggling sidebar')
        services.events.emit('event:sidebar-toggle')
    }

    $scope.signout = () => {
        security.principal.authenticate(null);
        services.state.go('app.login');
    }
}])


// ****************************************************************************
// Controller layout.sidebar 
//

m.controller('app.layout.sidebar', ['$scope', '$state', 'security', 'services', ($scope: any, $state: ng.ui.IState, security: ISecurity, services: IServices, settings: ISystemSettings) => {
    services.logger.debug('Loading app.layout.sidebar controller')

    $scope.$state = $state

    services.profile.me().then(me => {
        $scope.avataruri = me.avatar
        $scope.firstname = me.nameFirst
        $scope.isAdmin = me.isAdmin
    })

    $scope.signout = () => {
        security.principal.authenticate(null);
        services.state.go('app.login');
    }

    services.logger.debug(`Current state is: ${$scope.$state.name}`)
}])


// ****************************************************************************
// Controller layout.footer 
//

m.controller('app.layout.footer', ['$scope', '$state', 'security', 'services', ($scope: any, $state: ng.ui.IState, security: ISecurity, services: IServices) => {
    services.logger.debug('Loading app.layout.footer controller')
}])
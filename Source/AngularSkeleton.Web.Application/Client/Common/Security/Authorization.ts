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
// Interfaces 
//

interface IAuthorization {
    authorize(): void
}

// ****************************************************************************
// Module app.security.authorization
//

var m = angular.module('common.security.authorization', ['common.services.logger'])


// ****************************************************************************
// Factory app.security.authorization 
//

m.factory('authorization', ['$rootScope', '$http', '$state', 'principal', 'logger',
    ($rootScope, $http: ng.IHttpProvider, $state: ng.ui.IStateService, principal: IPrincipal, logger: ILogger) => {
        
        function authorize() {

            logger.debug('Authorizing')

            return principal.identity().then(() => {

                var isAuthenticated = principal.isAuthenticated()
                
                if ($rootScope.toState.data && $rootScope.toState.data.roles && $rootScope.toState.data.roles.length > 0 && !principal.isInAnyRole($rootScope.toState.data.roles)) {
                    
                    if (isAuthenticated) {
                        logger.debug(`Not authorized for desired state: ${$rootScope.toState}`)
                        $state.go('app.accessdenied') // user is signed in but not authorized for desired state
                    }
                    else {
                        logger.debug('User is not authenticated')
                        // user is not authenticated. stow the state they wanted before you
                        // send them to the signin state, so you can return them when you're done
                        $rootScope.returnToState = $rootScope.toState
                        $rootScope.returnToStateParams = $rootScope.toStateParams

                        // now, send them to the signin state so they can log in
                        logger.debug('Redirecting to log-on')
                        $state.go('app.login')
                    }
                }
            })
        }

        var authorization: IAuthorization = {
            authorize: authorize
        }

        return authorization
    }
]) 
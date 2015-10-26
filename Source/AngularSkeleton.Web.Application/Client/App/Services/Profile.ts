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
// Module app.services.profile
//

var m = angular.module('app.services.profile', ['app.repositories'])


// ****************************************************************************
// Interface IProfileService
//

interface IProfileService {
    me(): angular.IPromise<IUser>
}


// ****************************************************************************
// Profile
//

m.factory('profile', ['repositories', '$q', (repositories: IRepositories, $q) => {

    var user : IUser = null

    function me(): angular.IPromise<IUser> {
        const deferred = $q.defer()
        if (user == null) {
            repositories.users.me().then((data) => {
                user = data
                deferred.resolve(user)
            })
        } else {
            deferred.resolve(user)
        }
        return deferred.promise
    }

    var profile: IProfileService = {
        me: me
    }

    return profile
}])

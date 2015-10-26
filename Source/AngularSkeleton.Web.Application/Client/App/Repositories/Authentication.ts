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
// Interface IAuthenticationToken
//

interface IAuthenticationToken {
    access_token: string
    token_type: string
    expires_in: number
}


// ****************************************************************************
// Interface IAuthenticationRepository
//

interface IAuthenticationRepository {
    authenticate(authId: string, password: string, namespace?: string): angular.IPromise<IAuthenticationToken>
}


// ****************************************************************************
// Module app.repositories.authentication
//

var m = angular.module('app.repositories.authentication', ['restangular'])


// ****************************************************************************
// Authentication service
//

m.factory('authentication', ['Restangular', (restangular: restangular.IService) => {

    function authenticate(username: string, password: string) {
        const data = { grant_type: 'password', username: username, password: password }
        return <angular.IPromise<IAuthenticationToken>>restangular.one('accesstoken').customPOST(
            $.param(data), '', {}, { 'Content-Type': 'application/x-www-form-urlencoded' })
    }

    const repository: IAuthenticationRepository = {
        authenticate: authenticate
    }

    return repository;
}]) 
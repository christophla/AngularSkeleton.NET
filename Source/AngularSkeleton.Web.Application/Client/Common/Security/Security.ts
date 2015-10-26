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
// Interfaces app.security
//

interface ISecurity {
    authorization: IAuthorization
    principal: IPrincipal
}


// ****************************************************************************
// Module app.security
//

var m = angular.module('common.security', [
    'common.security.authorization',
    'common.security.principal'
])


// ****************************************************************************
// Service 'security'
//

m.factory('security', ['authorization', 'principal',  (authorization: IAuthorization, principal: IPrincipal) => {

        var security: ISecurity = {
            authorization: authorization,
            principal: principal
        }

        return security
    }
]) 
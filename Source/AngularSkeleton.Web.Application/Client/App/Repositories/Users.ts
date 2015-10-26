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
// Interface User
//
 
interface IUser extends restangular.IElement {
    id?: string
    archived?: boolean
    avatar: string
    email: string
    isAdmin: boolean
    lastLogin: Date
    nameFirst: string
    nameLast: string
    theme: string
    timezoneUtcOffset: number
    fullName(): string
}


// ****************************************************************************
// Interface IUsersRepository
//

interface IUsersRepository {
    create(user: IUser): angular.IPromise<IUser>
    find(id: string): angular.IPromise<IUser>
    getAll(): angular.IPromise<Array<IUser>>
    getAllArchived(): angular.IPromise<Array<IUser>>
    getAllActive(): angular.IPromise<Array<IUser>>
    me(): angular.IPromise<IUser>
    remove(user: IUser): angular.IPromise<any>
    save(user: IUser): angular.IPromise<any>
    toggle(user: IUser): angular.IPromise<any>
}


// ****************************************************************************
// Module app.repositories.users
//

var m = angular.module('app.repositories.users', ['restangular'])


// ****************************************************************************
// Users repository
//

m.factory('users', ['Restangular', (restangular: restangular.IService) => {

    // extend fullName()

    restangular.extendModel('users', (model : IUser) => {
        model.fullName = function() {
            return this.nameFirst + ' ' + this.nameLast
        }
        return model
    })
    
    // methods

    function create(user: IUser) {
        return <angular.IPromise<IUser>>restangular.all('manage').all('users').post(user)
    }

    function find(id: string) {
        return <angular.IPromise<IUser>>restangular.all('manage').one('users', id).get()
    }

    function getAll() {
        return restangular.all('manage').all('users').getList()
    }

    function getAllActive() {
        return restangular.all('manage').all('users').getList().then((data: Array<IUser>) => data.filter(o => o.archived === false))
    }

    function getAllArchived() {
        return restangular.all('manage').all('users').getList().then((data: Array<IUser>) => data.filter(o => o.archived))
    }

    function me() {
        return <angular.IPromise<IUser>>restangular.all('manage').all('users').customGET('me')
    }

    function remove(user: IUser) {
        return user.remove()
    }

    function save(user: IUser) {
        return user.put()
    }

    function toggle(user: IUser) {
        return user.customPOST(null, 'toggle')
    }

    var repository: IUsersRepository = {
        create: create,
        find: find,
        getAll: getAll,
        getAllActive: getAllActive,
        getAllArchived: getAllArchived,
        me: me,
        remove: remove,
        save: save,
        toggle: toggle
    }

    return repository
}]) 
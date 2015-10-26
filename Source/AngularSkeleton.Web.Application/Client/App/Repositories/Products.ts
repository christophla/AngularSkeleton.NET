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
// Interface IProduct
//

interface IProduct extends restangular.IElement {
    archived: boolean
    dateAdded: Date
    description: string
    id: number
    name: string
    quantityAvailable: number
}


// ****************************************************************************
// Interface IProductsRepository
//

interface IProductsRepository {
    create(product: IProduct): angular.IPromise<IProduct>
    find(id: string): angular.IPromise<IProduct>
    getAll(): angular.IPromise<Array<IProduct>>
    remove(product: IProduct): angular.IPromise<any>
    save(product: IProduct): angular.IPromise<any>
    toggle(product: IProduct): angular.IPromise<any>
}


// ****************************************************************************
// Module app.repositories.products
//

var m = angular.module('app.repositories.products', ['restangular'])


// ****************************************************************************
// Products repository
//

m.factory('products', ['Restangular', (restangular: restangular.IService) => {

    function create(client: IProduct) {
        return <angular.IPromise<IProduct>>restangular.all('manage').all('products').post(client)
    }

    function find(id: string) {
        return <angular.IPromise<IProduct>>restangular.all('manage').one('products', id).get()
    }

    function getAll() {
        return <angular.IPromise<Array<IProduct>>>restangular.all('manage').all('products').getList()
    }

    function remove(product: IProduct) {
        return product.remove()
    }

    function save(product: IProduct) {
        return product.put()
    }
    
    function toggle(product: IProduct) {
        return product.customPOST(null, 'toggle')
    }

    var repository: IProductsRepository = {
        create: create,
        find: find,
        getAll: getAll,
        remove: remove,
        save: save,
        toggle: toggle
    }

    return repository
}]) 
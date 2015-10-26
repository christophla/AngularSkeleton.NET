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
// Interface ICatalogEntry
//

interface ICatalogEntry extends restangular.IElement {
    archived: boolean
    dateAdded: Date
    description: string
    productId: number
    name: string
    quantityAvailable: number
}

// ****************************************************************************
// Interface ICatalogRepository
//

interface ICatalogRepository {
    search(criteria: string, skip: number, take: number): angular.IPromise<IPagedResult<ICatalogEntry>>
    entry(productId: number): angular.IPromise<ICatalogEntry>
}


// ****************************************************************************
// Module app.repositories.catalog
//

var m = angular.module('app.repositories.catalog', ['restangular'])


// ****************************************************************************
// Catalog repository
//

m.factory('catalog', ['Restangular', (restangular: restangular.IService) => {

    function search(criteria: string, skip: number, take: number) {
        return <angular.IPromise<IPagedResult<ICatalogEntry>>>restangular.all('catalog').customGET('search', { criteria: criteria, skip: skip, take: take })
    }

    function entry(productId: number) {
        return <angular.IPromise<ICatalogEntry>>restangular.all('catalog').one('entries', productId).get()
    }

    var repository: ICatalogRepository = {
        search: search,
        entry: entry
    } 

    return repository
}]) 
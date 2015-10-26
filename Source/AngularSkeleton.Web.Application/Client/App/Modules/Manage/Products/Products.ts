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
// Module app.manage.users
//

var m = angular.module('app.manage.products', [])


// ****************************************************************************
// Configure 
//

m.config(['$urlRouterProvider', '$stateProvider', 'settings', ($urlRouterProvider: ng.ui.IUrlRouterProvider, $stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $urlRouterProvider.when('/manage/products', '/manage/products/list')

    $stateProvider
        .state('app.manage.products', {
            abstract: false,
            url: '/products'
        })
        .state('app.manage.products.list', {
            url: '/list',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/manage/products/products.list.tpl.html`,
                    controller: 'app.manage.products.list'
                }
            },
            resolve: {
                products: ['repositories', (repositories: IRepositories) => repositories.products.getAll()]
            }
        })
        .state('app.manage.products.edit', {
            url: '/:productId',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/manage/products/products.edit.tpl.html`,
                    controller: 'app.manage.products.edit'
                }
            },
            resolve: {
                product: ['repositories', '$stateParams', (repositories: IRepositories, $stateParams: ng.ui.IStateParamsService) => repositories.products.find($stateParams['productId'])]
            }
        })
        .state('app.manage.products.new', {
            url: '/new',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/manage/products/products.add.tpl.html`,
                    controller: 'app.manage.products.add'
                }
            }
        })
}
])


// ****************************************************************************
// Controller app.manage.products.list
//

interface IProductsListScope extends ng.IScope {
    add(): void
    adding: boolean
    activeProducts: Array<IProduct>
    archivedProducts: Array<IProduct>
    calculateUsers(): void
    currentActivePage: number
    currentArchivedPage: number
    edit(user: IProduct): void
    itemsPerPage: number
    products: Array<IProduct>
    tab: string
}

m.controller('app.manage.products.list', ['$scope', 'repositories', 'products', 'services',
    ($scope: IProductsListScope, repositories: IRepositories, products: Array<IProduct>, services: IServices) => {

        services.logger.debug('Loaded controller app.manage.users.list')

        $scope.currentActivePage = 1
        $scope.currentArchivedPage = 1
        $scope.itemsPerPage = 5
        $scope.products = products
        $scope.tab = 'active'

        $scope.calculateUsers = () => {
            $scope.activeProducts = $scope.products.filter(product => !product.archived)
            $scope.archivedProducts = $scope.products.filter(product => product.archived)
        }
        $scope.calculateUsers()

        $scope.add = () => {
            $scope.adding = true
            services.state.go('app.manage.products.new')
        }

        $scope.edit = (product: IProduct) => {
            services.state.go('app.manage.products.edit', { productId: product.id })
        }
    }
])


// ****************************************************************************
// Controller app.manage.users.edit
//

interface IProductsEditScope extends ng.IScope {
    archive(user: IProduct): void
    product: IProduct
    reactivate(user: IProduct): void
    remove(user: IProduct): void
    save(isValid: boolean): void
    submitted: boolean
    submitting: boolean
}

m.controller('app.manage.products.edit', ['$scope', 'product', 'repositories', 'services',
    ($scope: IProductsEditScope, product: IProduct, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.manage.users.edit')

        $scope.product = product

        if ($scope.product == null) {
            services.logger.error('The product does not exist')
            services.location.path('/manage/products')
        }

        $scope.archive = (product: IProduct) => {
            if (product.archived) {
                services.logger.success('The product is already archived.')
                services.state.transitionTo('app.manage.products.list')
            }
            repositories.products.toggle(product).then(() => {
                services.logger.success('The product has been archived.')
                services.state.transitionTo('app.manage.products.list')
            }, response => {
                services.logger.error('An error occurred archiving the product', response.data)
            })
        }

        $scope.reactivate = (product: IProduct) => {
            if (!product.archived) {
                services.logger.success('The product is already active.')
                return
            }
            repositories.products.toggle(product).then(() => {
                services.logger.success('The user has been activated.')
                product.archived = false
            }, response => {
                services.logger.error('An error occurred activating the product.', response.data)
            })
        }

        $scope.remove = (product: IProduct) => {
            repositories.products.remove(product).then(() => {
                services.logger.success('The product has been removed.')
                services.state.transitionTo('app.manage.products.list')
            }, response => {
                services.logger.error('An error occurred removing the product', response.data)
            })
        }

        $scope.save = (isValid: boolean) => {
            $scope.submitted = true

            if (isValid) {
                $scope.submitting = true
                repositories.products.save($scope.product).then(() => {
                    services.logger.success('Updated product')
                    services.state.transitionTo('app.manage.products.list')
                }, response => {
                    services.logger.error('An error occurred updating the product', response.data)
                    $scope.submitted = false
                    $scope.submitting = false
                })
            }
        }
    }
]) 


// ****************************************************************************
// Controller app.manage.users.add
//

interface IProductsAddScope extends ng.IScope {
    product: IProduct
    save(isValid: boolean): void
    submitted: boolean
    submitting: boolean
}

m.controller('app.manage.products.add', ['$scope', 'repositories', 'services',
    ($scope: IProductsAddScope, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.manage.users.add')

        $scope.product = <IProduct> {}

        $scope.save = (isValid: boolean) => {

            $scope.submitted = true

            if (isValid) {
                $scope.submitting = true
                repositories.products.create($scope.product).then(() => {
                    services.logger.success('Created product')
                    services.state.transitionTo('app.manage.products.list')
                }, response => {
                    services.logger.error('An error occurred creating the product', response.data)
                    $scope.submitted = false
                    $scope.submitting = false
                })
            }
        }
    }
])

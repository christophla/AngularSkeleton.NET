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

var m = angular.module('app.manage.users', [])


// ****************************************************************************
// Configure 
//

m.config(['$urlRouterProvider', '$stateProvider', 'settings', ($urlRouterProvider: ng.ui.IUrlRouterProvider, $stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $urlRouterProvider.when('/manage/users', '/manage/users/list')

    $stateProvider
        .state('app.manage.users', {
            abstract: false,
            url: '/users'
        })
        .state('app.manage.users.list', {
            url: '/list',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/manage/users/users.list.tpl.html`,
                    controller: 'app.manage.users.list'
                }
            },
            resolve: {
                users: ['repositories', (repositories: IRepositories) => repositories.users.getAll()]
            }
        })
        .state('app.manage.users.edit', {
            url: '/:userId',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/manage/users/users.edit.tpl.html`,
                    controller: 'app.manage.users.edit'
                }
            },
            resolve: {
                user: ['repositories', '$stateParams', (repositories: IRepositories, $stateParams: ng.ui.IStateParamsService) => repositories.users.find($stateParams['userId'])]
            }
        })
        .state('app.manage.users.new', {
            url: '/new',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/manage/users/users.add.tpl.html`,
                    controller: 'app.manage.users.add'
                }
            }
        })
    }
])


// ****************************************************************************
// Controller app.manage.users.list
//

interface IUsersListScope extends ng.IScope {
    add(): void
    adding: boolean
    activeUsers: Array<IUser>
    archivedUsers: Array<IUser>
    calculateUsers(): void
    currentActivePage: number
    currentArchivedPage: number
    edit(user: IUser): void
    itemsPerPage: number
    tab: string
    users: Array<IUser>
}

m.controller('app.manage.users.list', ['$scope', 'repositories', 'users', 'services',
    ($scope: IUsersListScope, repositories: IRepositories, users: Array<IUser>, services: IServices) => {

        services.logger.debug('Loaded controller app.manage.users.list')

        $scope.currentActivePage = 1
        $scope.currentArchivedPage = 1
        $scope.itemsPerPage = 5
        $scope.users = users
        $scope.tab = 'active'

        $scope.calculateUsers = () => {
            $scope.activeUsers = $scope.users.filter(user => !user.archived)
            $scope.archivedUsers = $scope.users.filter(user => user.archived)
        }
        $scope.calculateUsers()

        $scope.add = () => {
            $scope.adding = true
            services.state.go('app.manage.users.new')
        }

        $scope.edit = (user: IUser) => {
            services.state.go('app.manage.users.edit', { userId: user.id })
        }
    }
])


// ****************************************************************************
// Controller app.manage.users.edit
//

interface IUsersEditScope extends ng.IScope {
    archive(user: IUser): void
    reactivate(user: IUser): void
    remove(user: IUser): void
    save(isValid: boolean): void
    submitted: boolean
    submitting: boolean
    user: IUser
}

m.controller('app.manage.users.edit', ['$scope', 'user', 'repositories', 'services',
    ($scope: IUsersEditScope, user: IUser, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.manage.users.edit')

        $scope.user = user

        if ($scope.user == null) {
            services.logger.error('The user does not exist')
            services.location.path('/manage/users')
        }

        $scope.archive = (user: IUser) => {
            if (user.archived) {
                services.logger.success('The user is already archived.')
                services.state.transitionTo('app.manage.users.list')
            }
            repositories.users.toggle(user).then(() => {
                services.logger.success('The user has been archived.')
                services.state.transitionTo('app.manage.users.list')
            }, response => {
                services.logger.error('An error occurred archiving the user', response.data)
            })
        }

        $scope.reactivate = (user: IUser) => {
            if (!user.archived) {
                services.logger.success('The user is already active.')
                return
            }
            repositories.users.toggle(user).then(() => {
                services.logger.success('The user has been activated.')
                user.archived = false
            }, response => {
                services.logger.error('An error occurred activating the user.', response.data)
            })
        }

        $scope.remove = (user: IUser) => {
            repositories.users.remove(user).then(() => {
                services.logger.success('The user has been removed.')
                services.state.transitionTo('app.manage.users.list')
            }, response => {
                services.logger.error('An error occurred removing the user', response.data)
            })
        }

        $scope.save = (isValid: boolean) => {
            $scope.submitted = true

            if (isValid) {
                $scope.submitting = true
                repositories.users.save($scope.user).then(() => {
                    services.logger.success('Updated user')
                    services.state.transitionTo('app.manage.users.list')
                }, response => {
                    services.logger.error('An error occurred updating the user', response.data)
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

interface IUsersAddScope extends ng.IScope {
    user: IUser
    save(isValid: boolean): void
    submitted: boolean
    submitting: boolean
}

m.controller('app.manage.users.add', ['$scope', 'repositories', 'services',
    ($scope: IUsersAddScope, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.manage.users.add')

        $scope.user = <IUser>{ }

        $scope.save = (isValid: boolean) => {

            $scope.submitted = true

            if (isValid) {
                $scope.submitting = true
                repositories.users.create($scope.user).then(() => {
                    services.logger.success('Created user')
                    services.state.transitionTo('app.manage.users.list')
                }, response => {
                    services.logger.error('An error occurred creating the user', response.data)
                    $scope.submitted = false
                    $scope.submitting = false
                })
            }
        }
    }
])

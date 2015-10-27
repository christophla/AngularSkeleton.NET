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
// Module app.dashboard
//
 
var m = angular.module('app.dashboard', [])
 

// ****************************************************************************
// Configure app.dashboard
//

m.config(['$stateProvider', 'settings', ($stateProvider: ng.ui.IStateProvider, settings: ISystemSettings) => {

    $stateProvider
        .state('app.dashboard', {
            url: '/dashboard',
            data: {
                roles: ['user']
            },
            controller: 'app.dashboard',
            views: {
                'content@': {
                    templateUrl: `${settings.moduleBaseUri}/dashboard/dashboard.tpl.html`,
                    controller: 'app.dashboard'
                }
            },
            resolve: {
                profile: ['services', (services: IServices) => services.profile.me()]
            }
        })
}])


// ****************************************************************************
// Controller app.dashboard
//

interface IDashboardScope {
    fullName: string
    graphdata: any
    labels: Array<string>
    popularCategories: Array<IDonutChartItem>
    series: Array<string>
    salesByLocation: Array<IDonutChartItem>
    salesByType: Array<IDonutChartItem>
}

m.controller('app.dashboard', ['$scope', 'profile', 'repositories', 'services',
    ($scope: IDashboardScope, profile: IUser, repositories: IRepositories, services: IServices) => {

        services.logger.debug('Loaded controller app.dashboard')
        
        $scope.fullName = profile.fullName()

        $scope.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July']
        $scope.series = ['New', 'Sold']

        $scope.graphdata = [
            { y: "2006", a: 5, b: 6 },
            { y: "2007", a: 10, b: 20 },
            { y: "2008", a: 18, b: 58 },
            { y: "2009", a: 33, b: 94 },
            { y: "2010", a: 25, b: 127 },
            { y: "2011", a: 40, b: 235 },
            { y: "2012", a: 45, b: 277 },
            { y: "2013", a: 57, b: 299 },
            { y: "2014", a: 72, b: 285 },
            { y: "2015", a: 64, b: 312 }
        ]

        $scope.popularCategories = [
            { label: "Software", value: 30 },
            { label: "Movies", value: 20 },
            { label: "Games", value: 15 },
            { label: "Other", value: 15 }
        ]

        $scope.salesByLocation= [
            { label: 'United States', value: 60 },
            { label: 'Europe', value: 30 },
            { label: 'Other', value: 10 }
        ]

        $scope.salesByType = [
            { label: "Download Sales", value: 90987 },
            { label: "In-Store Sales", value: 12988 }
        ]
    }
]) 
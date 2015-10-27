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
// Module app.widgets
//

var m = angular.module('app.widgets', [])


// ****************************************************************************
// Donut chart widget
//

interface IDonutChartItem {
    label: string
    value: number
}

m.directive('donutChartWidget', ['settings', (settings: ISystemSettings) => {
    
    return {
        bindToController: {
            data: '=',
            formatter: '@',
            title: '@'
        },     
        controller: () => {}, 
        controllerAs: 'ctrl',
        restrict: 'EA',    
        scope: { },
        templateUrl: `${settings.widgetsBaseUri}/templates/donutchart.widget.html`
    }
}])


// ****************************************************************************
// Graph widget
//
 
m.directive('graphWidget', ['settings', (settings: ISystemSettings) => {

    return { 
        bindToController: {
            data: '=',
            title: '@'
        },
        controller: () => { },
        controllerAs: 'ctrl',
        restrict: 'EA',
        scope: {},
        templateUrl: `${settings.widgetsBaseUri}/templates/graph.widget.html`
    }
}])
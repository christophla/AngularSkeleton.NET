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
// Donut chart
//

interface IDonutChartItem {
    label: string
    value: number
}

enum DonutChartFormatterType {
    Currency,
    Percent
}

m.directive('donutWidget', ['settings', (settings: ISystemSettings) => {
    
    return {
        restrict: 'EA', //E = element, A = attribute, C = class, M = comment         
        templateUrl: `${settings.widgetsBaseUri}/templates/donut.widget.html`,
        controller: () => {}, 
        controllerAs: 'ctrl',
        bindToController: {
            data: '=',
            formatter: '@',
            title: '@'
        },
        scope: {}
    }
}])
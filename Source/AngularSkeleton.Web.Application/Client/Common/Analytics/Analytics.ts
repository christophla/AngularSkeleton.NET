//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

declare var appInsights 

// ****************************************************************************
// Analytics Module 
//

var m = angular.module('common.analytics', [
    'angulartics'
]) 

// ****************************************************************************
// Configure 
//
 
m.config(['$analyticsProvider', $analyticsProvider => {

	$analyticsProvider.registerPageTrack(path => {
		appInsights.trackPageView(path)
	})

    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

	/**
     * Numeric properties are sent as metric (measurement) properties.
     * Everything else is sent as normal properties.
     */
	$analyticsProvider.registerEventTrack((eventName, eventProperties) => {
		var properties = {}
		var measurements = {}
        
		angular.forEach(eventProperties, (value, key) => {
			if (isNumeric(value)) {
				measurements[key] = parseFloat(value)
			} else {
				properties[key] = value
			}
		})

        appInsights.trackEvent(eventName, properties, measurements)
	})

}])

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
// Module common.loading
//
 
var m = angular.module('common.loading', [])


// ****************************************************************************
// Loading directive
//

m.directive('mAppLoading', $animate => {
        function link(scope, element, attributes) {
            // Due to the way AngularJS prevents animation during the bootstrap
            // of the application, we can't animate the top-level container; but,
            // since we added "ngAnimateChildren", we can animated the inner
            // container during this phase.
            // --
            // NOTE: Am using .eq(1) so that we don't animate the Style block.
            $animate.leave(element.children().eq(1)).then(
                () => {
                    // Remove the root directive element.
                    element.remove()
                    // Clear the closed-over variable references.
                    scope = element = attributes = null
                }
            )
        }

        // Return the directive configuration.
        return ({
            link: link,
            restrict: 'C'
        });
    }
);
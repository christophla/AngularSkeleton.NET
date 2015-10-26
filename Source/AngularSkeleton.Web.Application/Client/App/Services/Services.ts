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
// Interfaces app.services
//
 
interface IServices {
    events: IEvents
    location: ng.ILocationService
    logger: ILogger
    modal: ng.ui.bootstrap.IModalService
    profile: IProfileService
    state: ng.ui.IStateService
    underscore: UnderscoreStatic
}


// ****************************************************************************
// Module app.services
//

var m = angular.module('app.services', [
    'common.services.events',
    'common.services.logger',
    'common.services.underscore',
    'ui.bootstrap.modal',
    'ui.router',
    'app.services.profile'
])


// ****************************************************************************
// Service 'services'
//

m.factory('services', [
    'events',
    'logger',
    '$state',
    '$location',
    '$modal',
    'profile',
    '_',
    (
        events: IEvents,
        logger: ILogger,
        $state: ng.ui.IStateService,
        $location: ng.ILocationService,
        $modal: ng.ui.bootstrap.IModalService,
        profile: IProfileService,
        underscore: UnderscoreStatic
    ) => {

        var services: IServices = {
            events: events,
            location: $location,
            logger: logger,
            modal: $modal,
            profile: profile,
            state: $state,
            underscore: underscore
        }

        return services
    }
]) 
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
// Module app.filters
//

var m = angular.module('common.filters', [])

// ****************************************************************************
// Filter that formats hours from decimal format to hh:mm
//

m.filter('offset', () => (input, start: string) => {
    var result = parseInt(<any>start, 10)
    return input.slice(result)
}) 

// ****************************************************************************
// Filter that formats hours from decimal format to hh:mm
//

m.filter('hours', () => d => {
    d = d ? d : 0;
    var h = Math.floor(<number>d)
    var m = Math.round((d % 1) * 60).toString()
    if (m.length < 2) m = `0${m}`
    return h + ':' + m
})


// ****************************************************************************
// Filter that adds a (s) to an integer
//

m.filter('seconds', () => d => (d ? d + 's' : 'none'))


// ****************************************************************************
// Filter that adds (ms) to an integer
//

m.filter('milliseconds', () => d => (d ? d + 'ms' : 'none'))


// ****************************************************************************
// Filter that displays a formatted filesize (GB, MB, KB, B) for an integer
//

m.filter('filesize', () => fs => {
    if (fs >= 1073741824) {
        return Math.round(((fs / 1073741824) * Math.pow(10, 2)) / Math.pow(10, 2)) + ' GB'
    }
    if (fs >= 1048576) {
        return Math.round(((fs / 1048576) * Math.pow(10, 2)) / Math.pow(10, 2)) + ' MB'
    }
    if (fs >= 1024) {
        return Math.round(((fs / 1024) * Math.pow(10, 2)) / Math.pow(10, 2)) + ' KB'
    }
    return fs + ' B'
})


// ****************************************************************************
// Filter that adds a (%) to an integer
//

m.filter('percent', () => p => (p ? p + '%' : 'none'))


// ****************************************************************************
// Filter that formats an interger to persec
//

m.filter('persecond', () => p => (p ? p + '/s' : 'none'))

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
// Module app.forms
//

var m = angular.module('common.forms', ['common.filters'])


// ****************************************************************************
// Confirm dialog inline
//

m.directive('btnLoading', () => {
	return {
		link(scope: any, element, attrs) {
			scope.$watch(
				() => scope.$eval(attrs.btnLoading),
				value => {
					if (value) {
						element.addClass('disabled').attr('disabled', 'disabled')
						element.data('resetText', element.html())
						element.html(element.data('loading'))
					} else {
						element.removeClass('disabled').removeAttr('disabled')
						element.html(element.data('resetText'))
					}
				}
			)
		}
	}
})

// ****************************************************************************
// Confirm dialog inline
//

m.directive('confirmButton', [
	'$document', '$parse', ($document, $parse) => {
		return {
			restrict: 'A',
			link(scope: any, element, attrs) {
				var html, message, nope, title, yep

				var buttonId = Math.floor(Math.random() * 10000000000);

				attrs.buttonId = buttonId

				message = attrs.message || 'Are you sure?'
				yep = attrs.yes || 'Yes'
				nope = attrs.no || 'No'
				title = attrs.title || 'Confirm'

				html = `<div id="button-${buttonId}">\n  <span class="confirmbutton-msg">${message}</span><br>\n	<a class="confirmbutton-yes btn btn-danger">${yep}</a>\n	<a class="confirmbutton-no btn btn-primary">${nope}</a>\n</div>`

				element.popover({
					content: html,
					html: true,
					trigger: 'manual',
					title: title
				})

				return element.bind('click', e => {
					var dontBubble, pop
					dontBubble = true

					e.stopPropagation()

					element.popover('show')

					pop = $(`#button-${buttonId}`)

					pop.closest('.popover').click(e => {
						if (dontBubble) {
							e.stopPropagation()
						}
					})

					pop.find('.confirmbutton-yes').click(e => {
						dontBubble = false
						const func = $parse(attrs.confirmButton);
						func(scope)
					})

					pop.find('.confirmbutton-no').click(e => {
						dontBubble = false

						$document.off(`click.confirmbutton.${buttonId}`)

						element.popover('hide')
					})

					$document.on(`click.confirmbutton.${buttonId}`, ':not(.popover, .popover *)', () => {
						$document.off(`click.confirmbutton.${buttonId}`)
						element.popover('hide')
					})
				})
			}
		}
	}
])

// ****************************************************************************
// Currency
//

m.directive('money', () => {
	return {
		restrict: 'A',
		scope: {
			field: '='
		},
		replace: true,
		template: '<span><input type="text" ng-model="field"></input>{{field}}</span>',
		link(scope: any, element, attrs) {

			$(element).bind('keyup', e => {
				const input = element.find('input');
				const inputVal = input.val(); //clearing left side zeros
				while (scope.field.charAt(0) === '0') {
					scope.field = scope.field.substr(1)
				}

				scope.field = scope.field.replace(/[^\d.\',']/g, '')
				const point = scope.field.indexOf('.');
				if (point >= 0) { 
					scope.field = scope.field.slice(0, point + 3)
				}
				const decimalSplit = scope.field.split('.');
				var intPart = decimalSplit[0]
				var decPart = decimalSplit[1]

				intPart = intPart.replace(/[^\d]/g, '')
				if (intPart.length > 3) {
					let intDiv = Math.floor(intPart.length / 3);
					while (intDiv > 0) {
						let lastComma = intPart.indexOf(','); 
						if (lastComma < 0) {
							lastComma = intPart.length
						}
						if (lastComma - 3 > 0) {
							intPart = intPart.splice(lastComma - 3, 0, ',')
						}
						intDiv--
					}
				}

				if (decPart === undefined) {
					decPart = ''
				} else {
					decPart = `.${decPart}`
				}

				var res = intPart + decPart

				scope.$apply(() => { scope.field = res })
			})
		}
	}
})
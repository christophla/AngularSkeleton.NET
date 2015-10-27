//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

/* PARAMS */

var api_root = window['skeleton_api_root'];// = $_GET('api');
var client_root = window['skeleton_client_root'];// = $_GET('client');

/* LAB LOADER */

(function (o) { var K = o.$LAB, y = "UseLocalXHR", z = "AlwaysPreserveOrder", u = "AllowDuplicates", A = "CacheBust", B = "BasePath", C = /^[^?#]*\//.exec(location.href)[0], D = /^\w+\:\/\/\/?[^\/]+/.exec(C)[0], i = document.head || document.getElementsByTagName("head"), L = (o.opera && Object.prototype.toString.call(o.opera) == "[object Opera]") || ("MozAppearance" in document.documentElement.style), q = document.createElement("script"), E = typeof q.preload == "boolean", r = E || (q.readyState && q.readyState == "uninitialized"), F = !r && q.async === true, M = !r && !F && !L; function G(a) { return Object.prototype.toString.call(a) == "[object Function]" } function H(a) { return Object.prototype.toString.call(a) == "[object Array]" } function N(a, c) { var b = /^\w+\:\/\//; if (/^\/\/\/?/.test(a)) { a = location.protocol + a } else if (!b.test(a) && a.charAt(0) != "/") { a = (c || "") + a } return b.test(a) ? a : ((a.charAt(0) == "/" ? D : C) + a) } function s(a, c) { for (var b in a) { if (a.hasOwnProperty(b)) { c[b] = a[b] } } return c } function O(a) { var c = false; for (var b = 0; b < a.scripts.length; b++) { if (a.scripts[b].ready && a.scripts[b].exec_trigger) { c = true; a.scripts[b].exec_trigger(); a.scripts[b].exec_trigger = null } } return c } function t(a, c, b, d) { a.onload = a.onreadystatechange = function () { if ((a.readyState && a.readyState != "complete" && a.readyState != "loaded") || c[b]) return; a.onload = a.onreadystatechange = null; d() } } function I(a) { a.ready = a.finished = true; for (var c = 0; c < a.finished_listeners.length; c++) { a.finished_listeners[c]() } a.ready_listeners = []; a.finished_listeners = [] } function P(d, f, e, g, h) { setTimeout(function () { var a, c = f.real_src, b; if ("item" in i) { if (!i[0]) { setTimeout(arguments.callee, 25); return } i = i[0] } a = document.createElement("script"); if (f.type) a.type = f.type; if (f.charset) a.charset = f.charset; if (h) { if (r) { e.elem = a; if (E) { a.preload = true; a.onpreload = g } else { a.onreadystatechange = function () { if (a.readyState == "loaded") g() } } a.src = c } else if (h && c.indexOf(D) == 0 && d[y]) { b = new XMLHttpRequest(); b.onreadystatechange = function () { if (b.readyState == 4) { b.onreadystatechange = function () { }; e.text = b.responseText + "\n//@ sourceURL=" + c; g() } }; b.open("GET", c); b.send() } else { a.type = "text/cache-script"; t(a, e, "ready", function () { i.removeChild(a); g() }); a.src = c; i.insertBefore(a, i.firstChild) } } else if (F) { a.async = false; t(a, e, "finished", g); a.src = c; i.insertBefore(a, i.firstChild) } else { t(a, e, "finished", g); a.src = c; i.insertBefore(a, i.firstChild) } }, 0) } function J() { var l = {}, Q = r || M, n = [], p = {}, m; l[y] = true; l[z] = false; l[u] = false; l[A] = false; l[B] = ""; function R(a, c, b) { var d; function f() { if (d != null) { d = null; I(b) } } if (p[c.src].finished) return; if (!a[u]) p[c.src].finished = true; d = b.elem || document.createElement("script"); if (c.type) d.type = c.type; if (c.charset) d.charset = c.charset; t(d, b, "finished", f); if (b.elem) { b.elem = null } else if (b.text) { d.onload = d.onreadystatechange = null; d.text = b.text } else { d.src = c.real_src } i.insertBefore(d, i.firstChild); if (b.text) { f() } } function S(c, b, d, f) { var e, g, h = function () { b.ready_cb(b, function () { R(c, b, e) }) }, j = function () { b.finished_cb(b, d) }; b.src = N(b.src, c[B]); b.real_src = b.src + (c[A] ? ((/\?.*$/.test(b.src) ? "&_" : "?_") + ~~(Math.random() * 1E9) + "=") : ""); if (!p[b.src]) p[b.src] = { items: [], finished: false }; g = p[b.src].items; if (c[u] || g.length == 0) { e = g[g.length] = { ready: false, finished: false, ready_listeners: [h], finished_listeners: [j] }; P(c, b, e, ((f) ? function () { e.ready = true; for (var a = 0; a < e.ready_listeners.length; a++) { e.ready_listeners[a]() } e.ready_listeners = [] } : function () { I(e) }), f) } else { e = g[0]; if (e.finished) { j() } else { e.finished_listeners.push(j) } } } function v() { var e, g = s(l, {}), h = [], j = 0, w = false, k; function T(a, c) { a.ready = true; a.exec_trigger = c; x() } function U(a, c) { a.ready = a.finished = true; a.exec_trigger = null; for (var b = 0; b < c.scripts.length; b++) { if (!c.scripts[b].finished) return } c.finished = true; x() } function x() { while (j < h.length) { if (G(h[j])) { try { h[j++]() } catch (err) { } continue } else if (!h[j].finished) { if (O(h[j])) continue; break } j++ } if (j == h.length) { w = false; k = false } } function V() { if (!k || !k.scripts) { h.push(k = { scripts: [], finished: true }) } } e = { script: function () { for (var f = 0; f < arguments.length; f++) { (function (a, c) { var b; if (!H(a)) { c = [a] } for (var d = 0; d < c.length; d++) { V(); a = c[d]; if (G(a)) a = a(); if (!a) continue; if (H(a)) { b = [].slice.call(a); b.unshift(d, 1);[].splice.apply(c, b); d--; continue } if (typeof a == "string") a = { src: a }; a = s(a, { ready: false, ready_cb: T, finished: false, finished_cb: U }); k.finished = false; k.scripts.push(a); S(g, a, k, (Q && w)); w = true; if (g[z]) e.wait() } })(arguments[f], arguments[f]) } return e }, wait: function () { if (arguments.length > 0) { for (var a = 0; a < arguments.length; a++) { h.push(arguments[a]) } k = h[h.length - 1] } else k = false; x(); return e } }; return { script: e.script, wait: e.wait, setOptions: function (a) { s(a, g); return e } } } m = { setGlobalDefaults: function (a) { s(a, l); return m }, setOptions: function () { return v().setOptions.apply(null, arguments) }, script: function () { return v().script.apply(null, arguments) }, wait: function () { return v().wait.apply(null, arguments) }, queueScript: function () { n[n.length] = { type: "script", args: [].slice.call(arguments) }; return m }, queueWait: function () { n[n.length] = { type: "wait", args: [].slice.call(arguments) }; return m }, runQueue: function () { var a = m, c = n.length, b = c, d; for (; --b >= 0;) { d = n.shift(); a = a[d.type].apply(null, d.args) } return a }, noConflict: function () { o.$LAB = K; return m }, sandbox: function () { return J() } }; return m } o.$LAB = J(); (function (a, c, b) { if (document.readyState == null && document[a]) { document.readyState = "loading"; document[a](c, b = function () { document.removeEventListener(c, b, false); document.readyState = "complete" }, false) } })("addEventListener", "DOMContentLoaded") })(this);

$LAB
    .script('//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js').wait()
    .script('//cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.2/toastr.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.6/moment.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/underscore.js/1.8.3/underscore-min.js')

    .script('//cdnjs.cloudflare.com/ajax/libs/angular.js/1.4.6/angular.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/angular.js/1.4.6/angular-sanitize.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/angular.js/1.4.6/angular-animate.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/angular-ui-router/0.2.15/angular-ui-router.min.js')

    .script('//cdnjs.cloudflare.com/ajax/libs/angular-busy/4.1.3/angular-busy.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/angular-ui-bootstrap/0.13.3/ui-bootstrap-tpls.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/angular-ui-select/0.12.1/select.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/angular-moment/0.10.3/angular-moment.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/angulartics/0.20.0/angulartics.min.js')

    .script('//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/underscore.js/1.8.3/underscore-min.js')

    .script('//cdnjs.cloudflare.com/ajax/libs/restangular/1.5.1/restangular.min.js')

    .script('//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.min.js')
    .script('//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.4/raphael-min.js').wait()

    .script('//cdnjs.cloudflare.com/ajax/libs/Ladda/0.9.8/spin.min.js').wait()
    .script('//cdnjs.cloudflare.com/ajax/libs/Ladda/0.9.8/ladda.min.js').wait()

    .script(client_root + '/vendor/angular-ui-switch.js')
    .script(client_root + '/vendor/angular-ladda.js')
    .script(client_root + '/vendor/angular-morris-chart.js')
    .script(client_root + '/vendor/ngprogress.js')
    .script(client_root + '/vendor/anim-in-out.js').wait()
    .script(client_root + '/client.js');

/* ANGULARTICS AZURE INSIGHTS */

var appInsights = window.appInsights || function (config) {
    function r(config) { t[config] = function () { var i = arguments; t.queue.push(function () { t[config].apply(t, i) }) } } var t = { config: config }, u = document, e = window, o = "script", s = u.createElement(o), i, f; for (s.src = config.url || "//az416426.vo.msecnd.net/scripts/a/ai.0.js", u.getElementsByTagName(o)[0].parentNode.appendChild(s), t.cookie = u.cookie, t.queue = [], i = ["Event", "Exception", "Metric", "PageView", "Trace"]; i.length;) r("track" + i.pop()); return r("setAuthenticatedUserContext"), r("clearAuthenticatedUserContext"), config.disableExceptionTracking || (i = "onerror", r("_" + i), f = e[i], e[i] = function (config, r, u, e, o) { var s = f && f(config, r, u, e, o); return s !== !0 && t["_" + i](config, r, u, e, o), s }), t
}({
    instrumentationKey: "dd8bb107-47e9-48dd-8c52-3a5a4f936e44"
});

window.appInsights = appInsights;

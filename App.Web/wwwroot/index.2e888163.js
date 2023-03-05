var e=crypto;const t=e=>e instanceof CryptoKey;const r=new TextEncoder,a=new TextDecoder;function n(...e){const t=e.reduce(((e,{length:t})=>e+t),0),r=new Uint8Array(t);let a=0;return e.forEach((e=>{r.set(e,a),a+=e.length})),r}const o=e=>{const t=atob(e),r=new Uint8Array(t.length);for(let e=0;e<t.length;e++)r[e]=t.charCodeAt(e);return r},s=e=>{let t=e;t instanceof Uint8Array&&(t=a.decode(t)),t=t.replace(/-/g,"+").replace(/_/g,"/").replace(/\s/g,"");try{return o(t)}catch(e){throw new TypeError("The input to be decoded is not correctly encoded.")}};function i(){return"undefined"!=typeof WebSocketPair||"undefined"!=typeof navigator&&"Cloudflare-Workers"===navigator.userAgent||"undefined"!=typeof EdgeRuntime&&"vercel"===EdgeRuntime}class c extends Error{static get code(){return"ERR_JOSE_GENERIC"}constructor(e){var t;super(e),this.code="ERR_JOSE_GENERIC",this.name=this.constructor.name,null===(t=Error.captureStackTrace)||void 0===t||t.call(Error,this,this.constructor)}}class d extends c{static get code(){return"ERR_JWT_CLAIM_VALIDATION_FAILED"}constructor(e,t="unspecified",r="unspecified"){super(e),this.code="ERR_JWT_CLAIM_VALIDATION_FAILED",this.claim=t,this.reason=r}}class l extends c{static get code(){return"ERR_JWT_EXPIRED"}constructor(e,t="unspecified",r="unspecified"){super(e),this.code="ERR_JWT_EXPIRED",this.claim=t,this.reason=r}}class u extends c{constructor(){super(...arguments),this.code="ERR_JOSE_ALG_NOT_ALLOWED"}static get code(){return"ERR_JOSE_ALG_NOT_ALLOWED"}}class h extends c{constructor(){super(...arguments),this.code="ERR_JOSE_NOT_SUPPORTED"}static get code(){return"ERR_JOSE_NOT_SUPPORTED"}}class p extends c{constructor(){super(...arguments),this.code="ERR_JWS_INVALID"}static get code(){return"ERR_JWS_INVALID"}}class f extends c{constructor(){super(...arguments),this.code="ERR_JWT_INVALID"}static get code(){return"ERR_JWT_INVALID"}}Symbol.asyncIterator;class y extends c{constructor(){super(...arguments),this.code="ERR_JWS_SIGNATURE_VERIFICATION_FAILED",this.message="signature verification failed"}static get code(){return"ERR_JWS_SIGNATURE_VERIFICATION_FAILED"}}function m(e,t){const r=`SHA-${e.slice(-3)}`;switch(e){case"HS256":case"HS384":case"HS512":return{hash:r,name:"HMAC"};case"PS256":case"PS384":case"PS512":return{hash:r,name:"RSA-PSS",saltLength:e.slice(-3)>>3};case"RS256":case"RS384":case"RS512":return{hash:r,name:"RSASSA-PKCS1-v1_5"};case"ES256":case"ES384":case"ES512":return{hash:r,name:"ECDSA",namedCurve:t.namedCurve};case"EdDSA":return i()&&"NODE-ED25519"===t.name?{name:"NODE-ED25519",namedCurve:"NODE-ED25519"}:{name:t.name};default:throw new h(`alg ${e} is not supported either by JOSE or your javascript runtime`)}}var w=(e,t)=>{if(e.startsWith("RS")||e.startsWith("PS")){const{modulusLength:r}=t.algorithm;if("number"!=typeof r||r<2048)throw new TypeError(`${e} requires key modulusLength to be 2048 bits or larger`)}};function S(e,t="algorithm.name"){return new TypeError(`CryptoKey does not support this operation, its ${t} must be ${e}`)}function g(e,t){return e.name===t}function E(e){return parseInt(e.name.slice(4),10)}function A(e,t){if(t.length&&!t.some((t=>e.usages.includes(t)))){let e="CryptoKey does not support this operation, its usages must include ";if(t.length>2){const r=t.pop();e+=`one of ${t.join(", ")}, or ${r}.`}else 2===t.length?e+=`one of ${t[0]} or ${t[1]}.`:e+=`${t[0]}.`;throw new TypeError(e)}}function v(e,t,...r){switch(t){case"HS256":case"HS384":case"HS512":{if(!g(e.algorithm,"HMAC"))throw S("HMAC");const r=parseInt(t.slice(2),10);if(E(e.algorithm.hash)!==r)throw S(`SHA-${r}`,"algorithm.hash");break}case"RS256":case"RS384":case"RS512":{if(!g(e.algorithm,"RSASSA-PKCS1-v1_5"))throw S("RSASSA-PKCS1-v1_5");const r=parseInt(t.slice(2),10);if(E(e.algorithm.hash)!==r)throw S(`SHA-${r}`,"algorithm.hash");break}case"PS256":case"PS384":case"PS512":{if(!g(e.algorithm,"RSA-PSS"))throw S("RSA-PSS");const r=parseInt(t.slice(2),10);if(E(e.algorithm.hash)!==r)throw S(`SHA-${r}`,"algorithm.hash");break}case"EdDSA":if("Ed25519"!==e.algorithm.name&&"Ed448"!==e.algorithm.name){if(i()){if(g(e.algorithm,"NODE-ED25519"))break;throw S("Ed25519, Ed448, or NODE-ED25519")}throw S("Ed25519 or Ed448")}break;case"ES256":case"ES384":case"ES512":{if(!g(e.algorithm,"ECDSA"))throw S("ECDSA");const r=function(e){switch(e){case"ES256":return"P-256";case"ES384":return"P-384";case"ES512":return"P-521";default:throw new Error("unreachable")}}(t);if(e.algorithm.namedCurve!==r)throw S(r,"algorithm.namedCurve");break}default:throw new TypeError("CryptoKey does not support this operation")}A(e,r)}function b(e,t,...r){if(r.length>2){const t=r.pop();e+=`one of type ${r.join(", ")}, or ${t}.`}else 2===r.length?e+=`one of type ${r[0]} or ${r[1]}.`:e+=`of type ${r[0]}.`;return null==t?e+=` Received ${t}`:"function"==typeof t&&t.name?e+=` Received function ${t.name}`:"object"==typeof t&&null!=t&&t.constructor&&t.constructor.name&&(e+=` Received an instance of ${t.constructor.name}`),e}var k=(e,...t)=>b("Key must be ",e,...t);function R(e,t,...r){return b(`Key for the ${e} algorithm must be `,t,...r)}var P=e=>t(e);const C=["CryptoKey"];function T(r,a,n){if(t(a))return v(a,r,n),a;if(a instanceof Uint8Array){if(!r.startsWith("HS"))throw new TypeError(k(a,...C));return e.subtle.importKey("raw",a,{hash:`SHA-${r.slice(-3)}`,name:"HMAC"},!1,[n])}throw new TypeError(k(a,...C,"Uint8Array"))}var H=async(t,r,a,n)=>{const o=await T(t,r,"verify");w(t,o);const s=m(t,o.algorithm);try{return await e.subtle.verify(s,o,a,n)}catch(e){return!1}};var W=(...e)=>{const t=e.filter(Boolean);if(0===t.length||1===t.length)return!0;let r;for(const e of t){const t=Object.keys(e);if(r&&0!==r.size)for(const e of t){if(r.has(e))return!1;r.add(e)}else r=new Set(t)}return!0};function _(e){if("object"!=typeof(t=e)||null===t||"[object Object]"!==Object.prototype.toString.call(e))return!1;var t;if(null===Object.getPrototypeOf(e))return!0;let r=e;for(;null!==Object.getPrototypeOf(r);)r=Object.getPrototypeOf(r);return Object.getPrototypeOf(e)===r}var D=(e,t,r)=>{e.startsWith("HS")||"dir"===e||e.startsWith("PBES2")||/^A\d{3}(?:GCM)?KW$/.test(e)?((e,t)=>{if(!(t instanceof Uint8Array)){if(!P(t))throw new TypeError(R(e,t,...C,"Uint8Array"));if("secret"!==t.type)throw new TypeError(`${C.join(" or ")} instances for symmetric algorithms must be of type "secret"`)}})(e,t):((e,t,r)=>{if(!P(t))throw new TypeError(R(e,t,...C));if("secret"===t.type)throw new TypeError(`${C.join(" or ")} instances for asymmetric algorithms must not be of type "secret"`);if("sign"===r&&"public"===t.type)throw new TypeError(`${C.join(" or ")} instances for asymmetric algorithm signing must be of type "private"`);if("decrypt"===r&&"public"===t.type)throw new TypeError(`${C.join(" or ")} instances for asymmetric algorithm decryption must be of type "private"`);if(t.algorithm&&"verify"===r&&"private"===t.type)throw new TypeError(`${C.join(" or ")} instances for asymmetric algorithm verifying must be of type "public"`);if(t.algorithm&&"encrypt"===r&&"private"===t.type)throw new TypeError(`${C.join(" or ")} instances for asymmetric algorithm encryption must be of type "public"`)})(e,t,r)};var K=function(e,t,r,a,n){if(void 0!==n.crit&&void 0===a.crit)throw new e('"crit" (Critical) Header Parameter MUST be integrity protected');if(!a||void 0===a.crit)return new Set;if(!Array.isArray(a.crit)||0===a.crit.length||a.crit.some((e=>"string"!=typeof e||0===e.length)))throw new e('"crit" (Critical) Header Parameter MUST be an array of non-empty strings when present');let o;o=void 0!==r?new Map([...Object.entries(r),...t.entries()]):t;for(const t of a.crit){if(!o.has(t))throw new h(`Extension Header Parameter "${t}" is not recognized`);if(void 0===n[t])throw new e(`Extension Header Parameter "${t}" is missing`);if(o.get(t)&&void 0===a[t])throw new e(`Extension Header Parameter "${t}" MUST be integrity protected`)}return new Set(a.crit)};var O=(e,t)=>{if(void 0!==t&&(!Array.isArray(t)||t.some((e=>"string"!=typeof e))))throw new TypeError(`"${e}" option must be an array of strings`);if(t)return new Set(t)};async function I(e,t,o){var i;if(!_(e))throw new p("Flattened JWS must be an object");if(void 0===e.protected&&void 0===e.header)throw new p('Flattened JWS must have either of the "protected" or "header" members');if(void 0!==e.protected&&"string"!=typeof e.protected)throw new p("JWS Protected Header incorrect type");if(void 0===e.payload)throw new p("JWS Payload missing");if("string"!=typeof e.signature)throw new p("JWS Signature missing or incorrect type");if(void 0!==e.header&&!_(e.header))throw new p("JWS Unprotected Header incorrect type");let c={};if(e.protected)try{const t=s(e.protected);c=JSON.parse(a.decode(t))}catch(e){throw new p("JWS Protected Header is invalid")}if(!W(c,e.header))throw new p("JWS Protected and JWS Unprotected Header Parameter names must be disjoint");const d={...c,...e.header};let l=!0;if(K(p,new Map([["b64",!0]]),null==o?void 0:o.crit,c,d).has("b64")&&(l=c.b64,"boolean"!=typeof l))throw new p('The "b64" (base64url-encode payload) Header Parameter must be a boolean');const{alg:h}=d;if("string"!=typeof h||!h)throw new p('JWS "alg" (Algorithm) Header Parameter missing or invalid');const f=o&&O("algorithms",o.algorithms);if(f&&!f.has(h))throw new u('"alg" (Algorithm) Header Parameter not allowed');if(l){if("string"!=typeof e.payload)throw new p("JWS Payload must be a string")}else if("string"!=typeof e.payload&&!(e.payload instanceof Uint8Array))throw new p("JWS Payload must be a string or an Uint8Array instance");let m=!1;"function"==typeof t&&(t=await t(c,e),m=!0),D(h,t,"verify");const w=n(r.encode(null!==(i=e.protected)&&void 0!==i?i:""),r.encode("."),"string"==typeof e.payload?r.encode(e.payload):e.payload),S=s(e.signature);if(!await H(h,t,S,w))throw new y;let g;g=l?s(e.payload):"string"==typeof e.payload?r.encode(e.payload):e.payload;const E={payload:g};return void 0!==e.protected&&(E.protectedHeader=c),void 0!==e.header&&(E.unprotectedHeader=e.header),m?{...E,key:t}:E}async function J(e,t,r){if(e instanceof Uint8Array&&(e=a.decode(e)),"string"!=typeof e)throw new p("Compact JWS must be a string or Uint8Array");const{0:n,1:o,2:s,length:i}=e.split(".");if(3!==i)throw new p("Invalid Compact JWS");const c=await I({payload:o,protected:n,signature:s},t,r),d={payload:c.payload,protectedHeader:c.protectedHeader};return"function"==typeof t?{...d,key:c.key}:d}const $=86400,x=/^(\d+|\d+\.\d+) ?(seconds?|secs?|s|minutes?|mins?|m|hours?|hrs?|h|days?|d|weeks?|w|years?|yrs?|y)$/i;var j=e=>{const t=x.exec(e);if(!t)throw new TypeError("Invalid time period format");const r=parseFloat(t[1]);switch(t[2].toLowerCase()){case"sec":case"secs":case"second":case"seconds":case"s":return Math.round(r);case"minute":case"minutes":case"min":case"mins":case"m":return Math.round(60*r);case"hour":case"hours":case"hr":case"hrs":case"h":return Math.round(3600*r);case"day":case"days":case"d":return Math.round(r*$);case"week":case"weeks":case"w":return Math.round(604800*r);default:return Math.round(31557600*r)}};const M=e=>e.toLowerCase().replace(/^application\//,"");var N=(e,t,r={})=>{const{typ:n}=r;if(n&&("string"!=typeof e.typ||M(e.typ)!==M(n)))throw new d('unexpected "typ" JWT header value',"typ","check_failed");let o;try{o=JSON.parse(a.decode(t))}catch(e){}if(!_(o))throw new f("JWT Claims Set must be a top-level JSON object");const{issuer:s}=r;if(s&&!(Array.isArray(s)?s:[s]).includes(o.iss))throw new d('unexpected "iss" claim value',"iss","check_failed");const{subject:i}=r;if(i&&o.sub!==i)throw new d('unexpected "sub" claim value',"sub","check_failed");const{audience:c}=r;if(c&&(u=o.aud,h="string"==typeof c?[c]:c,!("string"==typeof u?h.includes(u):Array.isArray(u)&&h.some(Set.prototype.has.bind(new Set(u))))))throw new d('unexpected "aud" claim value',"aud","check_failed");var u,h;let p;switch(typeof r.clockTolerance){case"string":p=j(r.clockTolerance);break;case"number":p=r.clockTolerance;break;case"undefined":p=0;break;default:throw new TypeError("Invalid clockTolerance option type")}const{currentDate:y}=r,m=(w=y||new Date,Math.floor(w.getTime()/1e3));var w;if((void 0!==o.iat||r.maxTokenAge)&&"number"!=typeof o.iat)throw new d('"iat" claim must be a number',"iat","invalid");if(void 0!==o.nbf){if("number"!=typeof o.nbf)throw new d('"nbf" claim must be a number',"nbf","invalid");if(o.nbf>m+p)throw new d('"nbf" claim timestamp check failed',"nbf","check_failed")}if(void 0!==o.exp){if("number"!=typeof o.exp)throw new d('"exp" claim must be a number',"exp","invalid");if(o.exp<=m-p)throw new l('"exp" claim timestamp check failed',"exp","check_failed")}if(r.maxTokenAge){const e=m-o.iat;if(e-p>("number"==typeof r.maxTokenAge?r.maxTokenAge:j(r.maxTokenAge)))throw new l('"iat" claim timestamp check failed (too far in the past)',"iat","check_failed");if(e<0-p)throw new d('"iat" claim timestamp check failed (it should be in the past)',"iat","check_failed")}return o};async function U(e,t,r){var a;const n=await J(e,t,r);if((null===(a=n.protectedHeader.crit)||void 0===a?void 0:a.includes("b64"))&&!1===n.protectedHeader.b64)throw new f("JWTs MUST NOT use unencoded payload");const o={payload:N(n.protectedHeader,n.payload,r),protectedHeader:n.protectedHeader};return"function"==typeof t?{...o,key:n.key}:o}var L=async t=>{var r,a;if(!t.alg)throw new TypeError('"alg" argument is required when "jwk.alg" is not present');const{algorithm:n,keyUsages:o}=function(e){let t,r;switch(e.kty){case"oct":switch(e.alg){case"HS256":case"HS384":case"HS512":t={name:"HMAC",hash:`SHA-${e.alg.slice(-3)}`},r=["sign","verify"];break;case"A128CBC-HS256":case"A192CBC-HS384":case"A256CBC-HS512":throw new h(`${e.alg} keys cannot be imported as CryptoKey instances`);case"A128GCM":case"A192GCM":case"A256GCM":case"A128GCMKW":case"A192GCMKW":case"A256GCMKW":t={name:"AES-GCM"},r=["encrypt","decrypt"];break;case"A128KW":case"A192KW":case"A256KW":t={name:"AES-KW"},r=["wrapKey","unwrapKey"];break;case"PBES2-HS256+A128KW":case"PBES2-HS384+A192KW":case"PBES2-HS512+A256KW":t={name:"PBKDF2"},r=["deriveBits"];break;default:throw new h('Invalid or unsupported JWK "alg" (Algorithm) Parameter value')}break;case"RSA":switch(e.alg){case"PS256":case"PS384":case"PS512":t={name:"RSA-PSS",hash:`SHA-${e.alg.slice(-3)}`},r=e.d?["sign"]:["verify"];break;case"RS256":case"RS384":case"RS512":t={name:"RSASSA-PKCS1-v1_5",hash:`SHA-${e.alg.slice(-3)}`},r=e.d?["sign"]:["verify"];break;case"RSA-OAEP":case"RSA-OAEP-256":case"RSA-OAEP-384":case"RSA-OAEP-512":t={name:"RSA-OAEP",hash:`SHA-${parseInt(e.alg.slice(-3),10)||1}`},r=e.d?["decrypt","unwrapKey"]:["encrypt","wrapKey"];break;default:throw new h('Invalid or unsupported JWK "alg" (Algorithm) Parameter value')}break;case"EC":switch(e.alg){case"ES256":t={name:"ECDSA",namedCurve:"P-256"},r=e.d?["sign"]:["verify"];break;case"ES384":t={name:"ECDSA",namedCurve:"P-384"},r=e.d?["sign"]:["verify"];break;case"ES512":t={name:"ECDSA",namedCurve:"P-521"},r=e.d?["sign"]:["verify"];break;case"ECDH-ES":case"ECDH-ES+A128KW":case"ECDH-ES+A192KW":case"ECDH-ES+A256KW":t={name:"ECDH",namedCurve:e.crv},r=e.d?["deriveBits"]:[];break;default:throw new h('Invalid or unsupported JWK "alg" (Algorithm) Parameter value')}break;case"OKP":switch(e.alg){case"EdDSA":t={name:e.crv},r=e.d?["sign"]:["verify"];break;case"ECDH-ES":case"ECDH-ES+A128KW":case"ECDH-ES+A192KW":case"ECDH-ES+A256KW":t={name:e.crv},r=e.d?["deriveBits"]:[];break;default:throw new h('Invalid or unsupported JWK "alg" (Algorithm) Parameter value')}break;default:throw new h('Invalid or unsupported JWK "kty" (Key Type) Parameter value')}return{algorithm:t,keyUsages:r}}(t),c=[n,null!==(r=t.ext)&&void 0!==r&&r,null!==(a=t.key_ops)&&void 0!==a?a:o];if("PBKDF2"===n.name)return e.subtle.importKey("raw",s(t.k),...c);const d={...t};delete d.alg,delete d.use;try{return await e.subtle.importKey("jwk",d,...c)}catch(t){if("Ed25519"===n.name&&"NotSupportedError"===(null==t?void 0:t.name)&&i())return c[0]={name:"NODE-ED25519",namedCurve:"NODE-ED25519"},await e.subtle.importKey("jwk",d,...c);throw t}};async function G(e,t,r){var a;if(!_(e))throw new TypeError("JWK must be an object");switch(t||(t=e.alg),e.kty){case"oct":if("string"!=typeof e.k||!e.k)throw new TypeError('missing "k" (Key Value) Parameter value');return null!=r||(r=!0!==e.ext),r?L({...e,alg:t,ext:null!==(a=e.ext)&&void 0!==a&&a}):s(e.k);case"RSA":if(void 0!==e.oth)throw new h('RSA JWK "oth" (Other Primes Info) Parameter value is not supported');case"EC":case"OKP":return L({...e,alg:t});default:throw new h('Unsupported "kty" (Key Type) Parameter value')}}window.verify=async function(e){const t=await fetch("https://localhost:3000/api/auth/public-key"),r=await t.json(),a=await G(r,"ES256"),{payload:n,protectedHeader:o}=await U(e,a,{issuer:"app",audience:"app"});return n};
//# sourceMappingURL=index.2e888163.js.map

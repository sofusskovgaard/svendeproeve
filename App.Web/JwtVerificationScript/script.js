import { jwtVerify } from 'jose/dist/browser/jwt/verify'
import { importJWK } from 'jose/dist/browser/key/import'

async function verify(jwt) {
    const rawResponse = await fetch("https://localhost:3000/api/auth/public-key");
    const jwk = await rawResponse.json();
    const publicKey = await importJWK(jwk, "ES256");
    const { payload, protectedHeader } = await jwtVerify(jwt, publicKey, {
        issuer: "app",
        audience: "app"
    });
    return payload;
}

window.verify = verify;

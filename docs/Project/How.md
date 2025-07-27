# How Does It Work?

Client provides:

- `UserId` (required, query param)
- `CardNumber` (required, query param)
- API key in header: `X-API-KEY`

System returns:

- List of allowed actions for the card
- Card details used in resolution

Notes:

- Authorize in Swagger UI ("Authorize" button) using your API key.
- Each request returns actions for one specific card only.
- Missing or invalid API key = HTTP 401.
- Card not found = HTTP 404.
- All logic is handled by `CardResolver` and matrix rules.

Example:

- `UserId`: `User1`
- `CardNumber`: `Card12` or `Card121`

If User1 owns multiple cards (e.g. `Card12`, `Card121`), you get actions only for the card you specify in the request.
Other cards for the same user are not affected or returned.

How to test in Swagger:

- Go to `/swagger`
- Click **Authorize** and enter your API key
- Use GET `/api/v1/card` with `UserId` (e.g. `User1`) and `CardNumber` (e.g. `Card12`)


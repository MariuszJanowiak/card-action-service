# How Does It Work?

Client provides:

- `UserId` (required)
- `CardNumber` (required)

System returns:

- Resolved actions list
- Card details used in resolution

All logic is derived from a matrix system handled by `CardResolver`, which interprets card state and flags.

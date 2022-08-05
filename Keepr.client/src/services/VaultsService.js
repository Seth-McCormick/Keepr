import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

    async setActiveVault(id) {
        const res = await api.get(`api/vaults/${id}`)
        // logger.log(res.data)
        AppState.activeVault = res.data
    }

    async getVaultKeeps(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log("vaultkeep", res.data)
        AppState.vaultKeeps = res.data
        AppState.activeVaultId = id
    }

    async deleteVault(id) {
        const res = await api.delete(`api/vaults/${id}`)
        logger.log('deleted vault', res.data)
        AppState.usersVaults = AppState.usersVaults.filter(v => v.id != id)
    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        AppState.usersVaults = AppState.usersVaults.push(res.data)

        return res.data

    }

}
export const vaultsService = new VaultsService()
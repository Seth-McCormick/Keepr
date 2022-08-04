import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { keepsService } from "./KeepsService"
import { vaultsService } from "./VaultsService"

class VaultKeepsService {


    async remove(id) {
        const res = await api.delete(`api/vaultKeeps/${id}`)
        logger.log('Removed Keep', res.data)
        if (AppState.vaultKeeps.id = AppState.keeps.id) {
            AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.id != id)
        }
    }

    async addToVault(newVK) {
        const res = await api.post('api/vaultKeeps', newVK)
        if (AppState.activeVaultId == newVK.vaultId) {
            await vaultsService.getVaultKeeps(newVK.vaultId)
        }
        logger.log(res.data)

    }
}

export const vaultKeepsService = new VaultKeepsService()
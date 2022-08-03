import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
    async getProfile(id) {
        const res = await api(`api/profiles/${id}`)
        // logger.log(res.data)
        AppState.activeProfile = res.data
    }
    async getUsersVaults(id) {
        const res = await api(`api/profiles/${id}/vaults`)
        // logger.log(res.data)
        AppState.usersVaults = res.data
    }

    async getUsersKeeps(id) {
        const res = await api(`api/profiles/${id}/keeps`)
        // logger.log(res.data)
        AppState.usersKeeps = res.data
    }
}

export const profilesService = new ProfilesService()
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log(res.data)
        AppState.keeps = res.data
    }

    async setActiveKeep(keep) {
        // const res = await api.get(`api/keeps/${id}`)
        // logger.log(res.data)
        logger.log(keep)
        AppState.activeKeep = keep
        logger.log(AppState.activeKeep)
        // logger.log("active keep", res.data)
    }

    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log('keep created', res.data)

        AppState.usersKeeps.push(res.data)
        return res.data
    }

    async deleteKeep(id) {
        const res = await api.delete(`api/keeps/${id}`)
        logger.log("keep deleted", res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }

}

export const keepsService = new KeepsService()
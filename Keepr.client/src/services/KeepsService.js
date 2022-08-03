import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log(res.data)
        AppState.keeps = res.data
    }

    async setActiveKeep(id) {
        const res = await api.get(`api/keeps/${id}`)
        // logger.log(res.data)
        AppState.activeKeep = res.data
        // logger.log("active keep", res.data)
    }

    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log('keep created', res.data)


    }

}

export const keepsService = new KeepsService()
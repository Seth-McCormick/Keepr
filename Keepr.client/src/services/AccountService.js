import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyVaults() {
    try {
      const res = await api.get('/account/vaults')
      logger.log('my vaults', res.data)
      AppState.myVaults = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getMyKeeps() {
    try {
      const res = await api.get('/account/keeps')
      logger.log('my keeps', res.data)
      AppState.myKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }
}

export const accountService = new AccountService()

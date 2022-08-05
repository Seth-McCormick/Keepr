<template>

  <div class="col-md-3 mt-5 ms-5 d-flex">
    <img class="profile-img rounded" :src="account.picture" alt="">
    <div class="col-md-7 ms-3">
      <h1 class="">{{ account.name }}</h1>
      <h4>Vaults: {{ vaults.length }}</h4>
      <h4>Keeps: {{ keeps.length }}</h4>
    </div>
  </div>



  <div class="col-md-12 m-5 ">
    <div class="d-flex">
      <h1>Vaults</h1>
      <i class="mdi mdi-plus d-flex mdi-36px align-items-center selectable" title="Create Vault" data-bs-toggle="modal"
        data-bs-target="#vault-modal"></i>
    </div>
    <div>

      <div class="row vault-div">
        <Vault v-for="v in vaults" :key="v.id" :vault="v" />
      </div>
    </div>
  </div>



  <div class="col-md-12 m-5">
    <div class="d-flex">
      <h1>Keeps</h1>
      <i class="mdi mdi-plus d-flex mdi-36px align-items-center selectable" title="Create Keep" data-bs-toggle="modal"
        data-bs-target="#new-keep-modal"></i>
    </div>
    <div>
    </div>
    <div class="masonry-frame m-3">
      <div class="m-2" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>

</template>

<script>
import { useRoute, useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'

import { accountService } from '../services/AccountService'



export default {
  name: 'AccountPage',

  setup() {
    const router = useRouter()

    onMounted(async () => {

      try {
        await accountService.getMyVaults()
        await accountService.getMyKeeps()
      } catch (error) {
        Pop.toast(error, "error")
        logger.log(error)
        // router.push({ name: 'Home', params: { id: route.params.id } })
      }
    })
    return {

      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.usersKeeps),
      async getProfile() {
        try {
          await profilesService.getProfile()
        } catch (error) {
          Pop.toast(error, "error")
          logger.log(error)
        }
      }


    }
  }
}
</script>

<style scoped lang="scss">
.profile-img {
  width: 25%;
  height: 25%;
}

.masonry-frame {
  columns: 4;
}

div {
  break-inside: avoid;
  width: auto;
  height: auto;
}

@media (max-width: 756px) {
  .masonry-frame {
    columns: 2;
  }

}
</style>

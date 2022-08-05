<!-- <template>

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
      <i class="mdi mdi-plus d-flex align-items-center selectable" title="Create Vault" data-bs-toggle="modal"
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
      <i class="mdi mdi-plus d-flex align-items-center selectable" title="Create Keep" data-bs-toggle="modal"
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
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'ProfilePage',

  setup() {
    const route = useRoute()
    watchEffect(async () => {
      // should have a try catch
      // when it catches error...push back to the home page
      if (route.params.id) {

        await profilesService.getProfile(route.params.id),
          await profilesService.getUsersVaults(route.params.id),
          await profilesService.getUsersKeeps(route.params.id)
        // await vaultsService.getVaultKeeps(route.params.id)
      }
    })
    return {
      route,
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.usersVaults),
      keeps: computed(() => AppState.usersKeeps),
      account: computed(() => AppState.account),
      async getProfile() {
        try {
          await profilesService.getProfile(route.params.id)
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
</style> -->

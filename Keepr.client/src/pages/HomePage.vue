<template>
  <div class="masonry-frame m-3">
    <div class="m-2" v-for="k in keeps" :key="k.id">
      <Keep :keep="k" />
    </div>
  </div>

</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'

export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      await keepsService.getKeeps(),
        await profilesService.getUsersVaults()
    })

    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-frame {
  columns: 4;
  position: relative;
}

div {
  break-inside: avoid;

}


@media (max-width: 756px) {
  .masonry-frame {
    columns: 2;
  }
}
</style>

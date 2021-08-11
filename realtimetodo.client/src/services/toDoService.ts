import Vue, { PluginObject, VueConstructor } from 'vue'
import { LogLevel, HubConnectionBuilder, HubConnection } from '@microsoft/signalr'

export default class ToDoService {
  connection: HubConnection;

  constructor () {
    this.connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Trace)
      .withAutomaticReconnect()
      .withUrl('/hubs/todo')
      .build()
  }

  async start () {
    await this.connection.start()
  }

  async getLists () {
    const results = await this.connection.invoke('GetLists')

    return results
  }
}

export const ConnectionServices: PluginObject<any> = {
  install(Vue: VueConstructor<Vue>, option: any | undefined) {
    Vue.$connectionService = new ToDoService()
    Vue.prototype.$connectionService = Vue.$connectionService
  }
}



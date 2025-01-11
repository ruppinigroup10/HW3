// localhost vs production server

const config = {
  isLocalhost: false, // Switch this to false for production
  PORT: "7067", // Your local port
  version: "tar3", // Switch API version here, number of tar

  get baseApi() {
    console.log("Getting baseApi"); // Add this debug line
    return this.isLocalhost
      ? `https://localhost:${this.PORT}/api`
      : `https://proj.ruppin.ac.il/igroup10/test2/tar1/api`;
  },

  getApiUrl(endpoint) {
    console.log("Getting URL for endpoint:", endpoint); // Add this debug line
    endpoint = endpoint.replace(/^\/+/, "");
    const url = `${this.baseApi}/${endpoint}`;
    console.log("Generated URL:", url); // Add this debug line
    return url;
  },
};

console.log("Config loaded"); // Add this debug line
window.config = config;

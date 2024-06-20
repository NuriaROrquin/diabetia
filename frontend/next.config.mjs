/** @type {import('next').NextConfig} */
const nextConfig = {
    reactStrictMode: true,
    async redirects() {
        return [
            {
                source: "/",
                destination: "/auth/login",
                permanent: true,
            },
            {
                source: "/auth",
                destination: "/auth/login",
                permanent: true,
            },
        ];
    },
};

export default nextConfig;

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./app/**/*.{js,ts,jsx,tsx,mdx}",
    "./pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
      colors: {
        "blue-primary": "#648ECA",
        "blue-secondary": "#5D89C8",
        "blue-focus": "#80a8e1",
        "orange-primary": "#4CAF50",
        "orange-secondary": "#4CAF50",
        "orange-focus": "#66b669",
        "gray-primary": "#333",
        "gray-secondary": "#b1b1b1",
        "green-primary": "#4CAF50",
        "red-primary": "#BE0A1F"
      },
      backgroundImage: {
        "gradient-radial": "radial-gradient(var(--tw-gradient-stops))",
        "gradient-conic":
            "conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))",
      },
      backgroundColor: {
        "blue-primary": "#648ECA",
        "blue-secondary": "#5D89C8",
        "blue-focus": "#80a8e1",
        "orange-primary": "#4CAF50",
        "orange-secondary": "#4CAF50",
        "orange-focus": "#66b669",
        "gray-primary": "#333",
        "gray-secondary": "#b1b1b1",
        "green-primary": "#4CAF50",
        "red-primary": "#BE0A1F"
      },
      textColor: {
        "blue-primary": "#648ECA",
        "blue-secondary": "#5D89C8",
        "blue-focus": "#80a8e1",
        "orange-primary": "#4CAF50",
        "orange-secondary": "#4CAF50",
        "gray-primary": "#4D4D4D",
        "gray-secondary": "#b1b1b1",
        "green-primary": "#4CAF50",
        "red-primary": "#BE0A1F"
      },
      minHeight: {
        "80": "80vh"
      },
      height: {
        "600": "600px",
        "400": "400px",
      },
      backgroundImage: {
        'actividad-fisica': "url('/actividad-fisica.webp')",
        'food': "url('/food.jpg')",

      },
      animation: {
        'ping-slow': 'ping 3s cubic-bezier(0, 0, 0.2, 1) infinite',
      }
    },
  },
  plugins: [],
};

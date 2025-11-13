/* ================ 
   Base + Reset
=================== */

*,
*::before,
*::after {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

:root {
  --bg: #02030b;
  --bg-alt: #080b19;
  --card: #111426;
  --accent: #00e676;
  --accent-soft: rgba(0, 230, 118, 0.2);
  --danger: #ff5252;
  --text: #f8f9ff;
  --muted: #9aa0c0;
  --border: #232742;
  --radius-lg: 18px;
  --radius-md: 12px;
  --shadow-soft: 0 18px 40px rgba(0, 0, 0, 0.55);
  --shadow-subtle: 0 10px 25px rgba(0, 0, 0, 0.35);
}

html,
body {
  height: 100%;
}

body {
  font-family: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
  background: radial-gradient(circle at top, #181d42 0, #02030b 55%);
  color: var(--text);
  line-height: 1.5;
}

/* ================ 
   Layout
=================== */

main {
  max-width: 1100px;
  margin: 0 auto;
  padding: 96px 20px 72px;
}

/* ================ 
   Header / Nav
=================== */

.site-header {
  position: sticky;
  top: 0;
  z-index: 20;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 14px 22px;
  background: linear-gradient(
    to bottom,
    rgba(2, 3, 11, 0.96),
    rgba(2, 3, 11, 0.65)
  );
  backdrop-filter: blur(18px);
  border-bottom: 1px solid rgba(255, 255, 255, 0.05);
}

.logo {
  font-weight: 700;
  letter-spacing: 0.12em;
  text-transform: uppercase;
  font-size: 0.9rem;
}

.logo span {
  color: var(--accent);
}

.nav {
  display: flex;
  gap: 18px;
}

.nav a {
  font-size: 0.85rem;
  text-decoration: none;
  color: var(--muted);
  padding: 6px 10px;
  border-radius: 999px;
  transition: color 0.2s ease, background-color 0.2s ease, transform 0.15s ease;
}

.nav a:hover {
  color: var(--text);
  background: rgba(255, 255, 255, 0.06);
  transform: translateY(-1px);
}

/* ================ 
   Buttons
=================== */

.primary-btn,
.secondary-btn {
  border-radius: 999px;
  padding: 10px 20px;
  font-size: 0.9rem;
  border: none;
  cursor: pointer;
  font-weight: 500;
  letter-spacing: 0.03em;
  text-transform: uppercase;
  transition: transform 0.14s ease, box-shadow 0.14s ease, background 0.18s ease;
}

.primary-btn {
  background: linear-gradient(135deg, var(--accent), #5cf2a8);
  color: #02030b;
  box-shadow: var(--shadow-subtle);
}

.primary-btn:hover {
  transform: translateY(-1px);
  box-shadow: 0 15px 35px rgba(0, 0, 0, 0.6);
}

.secondary-btn {
  background: rgba(255, 255, 255, 0.06);
  color: var(--text);
  border: 1px solid rgba(255, 255, 255, 0.1);
}

.secondary-btn:hover {
  transform: translateY(-1px);
}

.secondary-btn.small {
  padding: 7px 14px;
  font-size: 0.8rem;
}

/* ================ 
   Hero
=================== */

.hero {
  min-height: 60vh;
  display: flex;
  align-items: center;
}

.hero-content {
  max-width: 600px;
  padding: 32px 22px;
  border-radius: var(--radius-lg);
  background: radial-gradient(
    circle at top left,
    rgba(0, 230, 118, 0.25) 0,
    #02030b 55%
  );
  box-shadow: var(--shadow-soft);
  border: 1px solid rgba(255, 255, 255, 0.12);
  position: relative;
}

.hero-content h1 {
  position: relative;
  font-size: clamp(2.2rem, 4vw, 2.9rem);
  margin-bottom: 10px;
}

.hero-content p {
  position: relative;
  color: var(--muted);
  margin-bottom: 20px;
  font-size: 0.95rem;
}

/* ================ 
   Sections
=================== */

section {
  margin-bottom: 72px;
}

section h2 {
  font-size: 1.5rem;
  margin-bottom: 12px;
}

section p {
  color: var(--muted);
  font-size: 0.95rem;
}

/* ================ 
   Game Section
=================== */

.game-section {
  background: var(--bg-alt);
  border-radius: var(--radius-lg);
  padding: 22px 16px 20px;
  box-shadow: var(--shadow-soft);
  border: 1px solid rgba(255, 255, 255, 0.08);
}

.game-layout {
  display: grid;
  grid-template-columns: 2fr 1.3fr;
  gap: 18px;
}

/* Panel Base */

.game-panel {
  border-radius: var(--radius-md);
  border: 2px solid var(--border);
  background: #050616; /* default dark */
  min-height: 220px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: var(--shadow-subtle);
  cursor: pointer;
  transition: background-color 0.15s ease, transform 0.1s ease, box-shadow 0.1s ease;
}

.panel-message {
  text-align: center;
  padding: 0 18px;
  font-size: 1.1rem;
  font-weight: 500;
  color: #ffffff;
}

/* STRONG Color States */

.game-panel.waiting {
  background-color: #ffb300; /* bright yellow/orange */
}

.game-panel.ready {
  background-color: #00e676; /* bright green */
}

.game-panel.too-soon {
  background-color: #ff5252; /* bright red */
}

/* Sidebar */

.game-sidebar {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.stat-card {
  border-radius: var(--radius-md);
  border: 1px solid var(--border);
  padding: 10px 12px;
  background: #050616;
  box-shadow: var(--shadow-subtle);
}

.stat-card h3 {
  font-size: 0.9rem;
  text-transform: uppercase;
  letter-spacing: 0.08em;
  color: var(--muted);
  margin-bottom: 6px;
}

.stat-value {
  font-size: 1.4rem;
  display: flex;
  align-items: baseline;
  gap: 4px;
}

.stat-value .unit {
  font-size: 0.8rem;
  color: var(--muted);
}

.stat-note {
  margin-top: 4px;
  font-size: 0.8rem;
  color: var(--muted);
}

.history-list {
  list-style: none;
  max-height: 100px;
  overflow-y: auto;
  font-size: 0.85rem;
  color: var(--muted);
}

.history-list li {
  display: flex;
  justify-content: space-between;
  padding: 2px 0;
}

.feedback-text {
  margin-top: 4px;
  font-size: 0.85rem;
  color: var(--muted);
}

/* About */

.about {
  padding: 10px 4px 0;
}

/* Footer */

.site-footer {
  border-top: 1px solid rgba(255, 255, 255, 0.08);
  padding: 16px 22px 22px;
  text-align: center;
  font-size: 0.8rem;
  color: var(--muted);
}

/* Responsive */

@media (max-width: 900px) {
  .game-layout {
    grid-template-columns: 1fr;
  }
}

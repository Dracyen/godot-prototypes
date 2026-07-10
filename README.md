# godot-prototypes
Um repositório dedicado à exploração de padrões de desenho de software (Design Patterns), algoritmos de simulação de inteligência artificial e matemática aplicada em ambiente 2.5D e 3D no motor Godot Engine.

## 🛠️ Tecnologias Utilizadas
* **Motor de Jogo:** Godot Engine
* **Linguagem:** GDScript / C#

## 📐 Pilares de Desenvolvimento

### 1. Desacoplamento de Input (`Command Pattern Test`)
* **O que faz:** Um protótipo focado estritamente em Arquitetura de Software.
* **Lógica Técnica:** Implementação do padrão de desenho *Command* (GoF). As ações do jogador são transformadas em objetos encapsulados, permitindo que os mesmos botões do teclado ou comando executem ações completamente diferentes dependendo do estado ou "modo" ativo do jogo (ex: Modo Exploração vs. Modo Menu), eliminando estruturas condicionais de código esparguete (`if/else` gigantes).

### 2. Simulação e Sistemas Interativos (`Time Waster`)
* **Pathfinding 3D:** Implementação de navegação de agentes baseada em malhas de navegação (NavigationMeshes) para movimentação inteligente em cenários complexos.
* **Sistema de Câmaras Estilo RPG Clássico:** Algoritmo de cálculo trigonométrico em tempo real que analisa a relação entre o vetor de movimento do jogador e o ângulo atual da câmara para alternar dinamicamente o *sprite* bidimensional correto da personagem (inspirado em clássicos como *Breath of Fire 3*).

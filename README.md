# CoronaCad-2.0
Plugin de Autocad  para visualizar os números de  casos de COVID-19 no Brasil, por estado ou municípios.

Os dados utlizados para plotagem dos dados podem ser baixados no site desenvolvido pelo Governo Federal, que serve como veículo oficial de comunicação sobre a situação epidemiológica da COVID-19 no Brasil, https://covid.saude.gov.br/.

O Plugin plota os dados brutos disponibilizados em arquivos .csv e também plota uma média móvel calculada de 7 dias. Os dados disponíveis são Casos Novos, Casos Acumulados, Óbitos Novos, Óbitos Acumulados e Novos Recuperados, todos podem ser separados em estados, municípios ou acumulados para o Brasil.

O arquivo para carregar o plugin pode ser baixado na pasta principal desse repositório em formato .ddl e pode ser carregado no Autocad através do comando "NETLOAD". Foram feitos teste nas versões a partir do Autocad 2017.
Muito provavelmente haverá problemas ao carregar o plugin no autocad se nunca foi carregado outro anteriormente, este problema é devido Windows bloquiar a execução de arquivos DLLs criados em outra máquina. Então também na pasta principal deixo um tutorial para resolver.

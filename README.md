# Разработка игровых сервисов
Отчет по лабораторной работе #3 выполнил(а):
- Фалалеев Вадим Эдуардович
- РИ-300012

Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.

## Цель работы
Интеграция интерфейса пользователя в разрабатываемое интерактивное приложение.

## Задание 1
### Используя видео-материалы практических работ 1-2 повторить реализацию игровых механик:

– 1 Практическая работа «Реализация механизма ловли объектов».

– 2 Практическая работа «Реализация графического интерфейса с добавлением счетчика очков».

Ход работы:

- Реализуем движение сферы за указателем мыши. Для этого создадим скрипт EnergyShield, поставим его на объект EnergyShield и напишем следующий код.

```c#

using UnityEngine;

public class EnergyShield : MonoBehaviour
{

    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
}

```

- Следующим шагом сделаем уничтожение яиц при столкновении со сферой. Добавим в прошлый скрипт новый метод, реализующий данные действия.

```c#

private void OnCollisionEnter(Collision collision)
{
    GameObject collided = collision.gameObject;
    if (collided.tag == "Dragon Egg")
        Destroy(collided);
}

```

- Последним действием первого урока будет добавление интерфейса для будущего подсчета очков. Создаем объект Canvas, нажимая в списке сцены ПКМ, UI -> Text -> TextMeshPro. Переименуем объект внутри Canvas на Score. В компоненте Canvas объекта Canvas поменяем Render Mode на Screen Space - Camera, и в Render Camera поставим объект Main Camera со сцены. Plane Distance изменим на 10. 

![image](https://user-images.githubusercontent.com/54228342/196152589-e958f94d-428f-4142-b716-b2b3d75888c1.png)

- В компоненте Rect Transform объекта Score поставим якорь на верхний правый угол, Pos X и Pos Y на -50, Width на 120, Height на 50. В комопненте TextMeshPro - Text (UI) запишем текст "Score:", а Font Size поставим на 32.

![image](https://user-images.githubusercontent.com/54228342/196153473-c04ef97e-2440-43ca-bb5e-76d83a952d74.png)

- После всех совершенных действий посмотрим на итог первого видео.

![Видео 17-10-2022 163329](https://user-images.githubusercontent.com/54228342/196167243-849818b6-6454-412f-a274-0ce0f243310b.gif)

- Перейдем к практике во втором видео. Продолжим работать с пользовательским интерфейсом, сделав счетчик очков рабочим. Вернемся в скрипт EnergyShield. Подключим новую библиотеку и добавим метод Start. Начало скрипта будет выглядить следующим образом:

```c#

using UnityEngine;
using TMPro;

public class EnergyShield : MonoBehaviour
{
    public TextMeshProUGUI scoreGT;

    private void Start()
    {
        GameObject scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = "0";
    }
    
    ...

```

- Теперь в конец метода OnCollisionEnter добавим строки, чтобы счет обновлялся при поимке яиц:

```c#

int score = int.Parse(scoreGT.text);
score += 1;
scoreGT.text = score.ToString();

```

- Зайдем в скрипт DragonEgg и там в методе Update добавим строки внутри условия:

```c#

DragonPicker apScript = Camera.main.GetComponent<DragonPicker>();
apScript.DragonEggDestroyed();

```

- После этого заходим в скрипт DragonPicker, создавая новый метод:

```c#

public void DragonEggDestroyed()
{
    GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
    foreach (GameObject tGO in tDragonEggArray)
        Destroy(tGO);
}

```

- Посмотрим на результат работы после второго видео. Счетчик работает и все яйца удаляются, если одно пролетело мимо сферы.

![Видео 17-10-2022 175315](https://user-images.githubusercontent.com/54228342/196182597-fb6f8844-0a6e-4c35-bd06-be1adbe44afe.gif)

## Задание 2
### Используя видео-материалы практических работ 3-4 повторить реализацию игровых механик:

– 3 Практическая работа «Уменьшение жизни. Добавление текстур».

– 4 Практическая работа «Структурирование исходных файлов в папке».

Ход работы:

- Добавим уменьшение нашего здоровья, которым является количество наших щитов, и перезагрузку уровня при проигрыше. Зайдем в скрипт DragonPicker и добавим новые строки в код(они выделены комментариями).

```c#

using System.Collections.Generic; // new
using UnityEngine;
using UnityEngine.SceneManagement; // new

public class DragonPicker : MonoBehaviour
{
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6;
    public float energyShieldRadius = 1.5f;
    public List<GameObject> shieldList; // new

    private void Start()
    {
        shieldList = new(); // new

        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate(energyShieldPrefab);
            tShieldGo.transform.position = new(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new(1 * i, 1 * i, 1 * i);
            shieldList.Add(tShieldGo); // new
        }
    }

    public void DragonEggDestroyed()
    {
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray)
            Destroy(tGO);

        int shieldIndex = shieldList.Count - 1; // new
        GameObject tShieldGo = shieldList[shieldIndex]; // new
        shieldList.RemoveAt(shieldIndex); // new
        Destroy(tShieldGo); // new
        
        if (shieldList.Count == 0) // new
            SceneManager.LoadScene("_0Scene"); // new
    }
}

```

- Теперь сделаем фон для игры. Скачаем бесплатный ассет Autumn Mountain из Asset Store и импортируем его в проект.

![image](https://user-images.githubusercontent.com/54228342/196334350-0c7cc301-cfec-4ecd-8372-518a96ca7b6d.png)

- После этого заходим в папку FreeMountain -> Prefabs. Копируем префаб, переименовываем в DragonMountain, перемещаем в папку Scenes, и добавляем его на сцену. Изменим положение объекта на сцене, чтобы он стоял гармонично.
- Перенесем папку FreeMountain -> SkyBox в папку Scenes. После этого нажимаем Window -> Rendering -> Lighting. Появившееся окно прикрепляем рядом с инспектором. Заходим во вкладку Environment и в Skybox Material ставим Free_skybox из ассета.
- Проверяем, как выглядет сцена, после всех совершенных действий из третьего видео. Теперь у нас теряются жизни и уменьшается сфера, а после потери всех жизней перезагружается сцена. Вместо серого фона теперь стоит гора и синее небо.

![Видео 18-10-2022 093339](https://user-images.githubusercontent.com/54228342/196336911-ed0ca3e2-b49b-4c71-8c5b-c253780e5217.gif)

- Следующим делом нужно правильно структурировать файлы в проекте, так как хранить разные типы файлов в одной папке является не совсем верным. Для этого создадим все нужные папки для сортировки. Назовем их _Animations, _AudioFiles, _Materials, _Prefabs, _Scripts и _Textures. Переименуем папку Scenes в _Scenes. Далее все файлы используемых анимаций кладем в первую папку, материалов в третью, и так далее, пока все используемые файлы проекта не будут находиться в своих папках. После этого удалим все папки ассетов, которые мы импортировали - другие файлы их тех папок нам больше не пригодятся. После этого у нас получится проект с правильно структурированными файлами, в папках которых в будущем будет легче ориентироваться.
- Выполнив задание из четвертого видео, папка проекта должна выглядеть примерно следующим образом:

![image](https://user-images.githubusercontent.com/54228342/196399654-1cb04f87-e1ae-4065-a4a5-24f46b0837cb.png)

## Задание 3
### Используя видео-материалы практических работ 5 повторить реализацию игровых механик:

– 5 Практическая работа «Интеграция игровых сервисов в готовое приложение».

Ход работы:

- Последним заданием для данной лабораторной работы является создание готового рабочего приложения. Для этого нужно создать билд внутри Unity. Этот этап пропущу, так как я это уже делал во втором задании второй лабораторной работы. Добавлю лишь то, что нужно зайти в Player Settings, и во вкладке Player поставить Lightmap Encoding на Normal Quality. Так же нужно поставить Compression Format на Disabled. 
- Далее импортируем специальный плагин для работы приложения с сервисом Яндекс.Игры. Его можно скачать в интернете и загрузить в проект.

![image](https://user-images.githubusercontent.com/54228342/196421737-28aa2534-fc9b-4781-bde3-9e86764541b8.png)

- После этого наверху перед кнопкой Window появится кнопка YG. Нужно нажать на нее и выбрать Create YandexGame Object, после чего на сцене появится специальный объект. Если в проекте несколько сцен, тогда такой объект должен быть на каждой. 

![image](https://user-images.githubusercontent.com/54228342/196422134-75b356f8-8501-44db-92ef-fd9f353028db.png)

- Затем нужно вернуться в настройки проекта и выбрать новый шаблон от плагина.

![image](https://user-images.githubusercontent.com/54228342/196422383-6bc3151c-5168-4f38-9090-4c0e43254772.png)

- Билдим проект, выбирая нужную папку для билда. Заходим через Visual Studio в html файл билда, чтобы проверить, правильно ли работает плагин. Видим, что в файле есть методы от плагина, значит все работает корректно.

![image](https://user-images.githubusercontent.com/54228342/196423161-caa367fe-7c3c-4bae-ac66-abfb66870e3e.png)

- Остается лишь архивировать все содержимое папки в .zip и отправить на черновик в Яндекс.Игры. После некоторого ожидания можно открыть ссылку на черновик и убедиться, что все сделано верно и игра работает на сервисе.

![image](https://user-images.githubusercontent.com/54228342/196423901-2a979070-0360-4022-a554-3b9ad1d8cc9a.png)

## Выводы

- После выполнения третьей лабораторной работы я научился создавать механику ловли объектов, траты здоровья и перезапуска сцены. Поработал с пользовательским интерфейсом, научился его программно изменять(подсчет очков). Работал с несколькими ассетами, а именно с текстурами и материалами. Структурировал файлы проекта по папкам и интегрировал плагин для работы приложения с сервисом Яндекс.Игры.

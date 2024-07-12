# Unity 6 - 3D Project Template
*Updated June, 2024*

**This project template for all Unity 6 3D projects in the book<br>
*Introduction to Game Design, Prototyping, and Development - 4^th^ Edition*.<br><br>
This Git repo includes:**
* A **Unity project template** that includes the correct .gitignore and .gitattributes files to work with GitLab.MSU.edu. You won't see the .git___ files because they are invisible, but they help GIT work well with Unity projects.
* This **README.md MarkDown file.** You will need to edit the **Required ReadMe Info** section below for ***EVERY*** project in this class.
* A **UnityWindowLayout.wlt file** in the project folder (the same folder as this ReadMe.md file) that you can load to lay out the Unity window the way that Jeremy recommends.
* **Modifications to the base Unity project that *remove* Plastic SCM**, which is extremely helpful for allowing Git to work well.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *I hope this template works for you. If it doesn't, please let us know on Piazza. – Jeremy*

---

# Required ReadMe Info for ALL MI 231 Projects
* **Project**   - 
* **Your Name** - 
* **Date**      - 

<br>

1. **What are the controls to your game? How do we play?**



<br>

2. **What creative additions did you make? How can we find them?**



<br>

3. **Any assets used that you didn't create yourself?** <br> (art, music, etc. Just tell us where you got it, link it here)



<br>

4. **Did you receive help from anyone outside this class?** <br> (list their names and what they helped with)



<br>

5. **Did you get help from any AI Code Assistants?** <br> (Tell us which .cs file to look in for the citation and describe what you learned)



<br>

6. **Did you get help from any online websites, videos, or tutorials?** <br> (link them here)



<br>

7. **What trouble did you have with this project?**



<br>

8. **Is there anything else we should know?**




<br><br><br><br><br>

---

# Connecting GitHub Desktop to GitLab.MSU.edu Projects
In MSU Media+Information classes, I recommend using **GitHub Desktop** ([https://desktop.github.com/](https://desktop.github.com/)) to manage your GIT repos. However, doing so does require some (annoying) initial setup. *Thank you to Chris Cardimen for posting these setup steps to the MI 497 Discord channel.*

1. Download GitHub Desktop: [**https://desktop.github.com/**](https://desktop.github.com/)
2. Sign in using whatever GitHub account you prefer, I'm using my school GitHub account. You can't sign in with your GitLab account, this is a GitHub application. Linking the two comes after. If you don't have a GitHub account, you can make one for free [**here**](https://docs.github.com/en/get-started/signing-up-for-github/signing-up-for-a-new-github-account) by clicking the **Sign up** button at the top and following the directions.
3. [Login to **GitLab.msu.edu** on a web browser](http://gitlab.msu.edu).
4. Click **'Preferences'** under your profile picture (which is in the top-right corner of the screen).
5. On the lefthand sidebar, click **'Access Tokens'**
	1. Type in a **Token name** (it can be anything).
	2. Leave the **Expiration date** blank
	3. Under **Select scopes** select only **'api'**.
	4. Click **Create personal access token**.
6. You'll get a popup telling you the token ID. **_You ONLY get to see this token ID once. Copy and paste it somewhere or WRITE IT DOWN!!_**
7. **To clone a project into GitHub Desktop:**
	1. Go to the main GitLab repository page (e.g., gitlab.msu.edu/mi231-f22/[your name]/[project name]) and click **'Clone'**.
	2. Click the clipboard copy icon next to **'Clone with HTTPS'** to copy the link.
	3. Return to **GitHub Desktop**. From the **File** menu, choose **'Clone Repository** or click the **'Clone Repository'** button.
	4. Click the **URL** tab.
	5. Input the HTTPS link that you just copied as the Repository URL.
	6. Choose whatever local path you want *(I recommend your 'MI 231' folder on your computer)*.
	7. Click **'Clone'**.
	8. It's going to prompt you for login information.
		* For username, use your GitLab username (e.g., gameprof@msu.edu).
		* For the passcode/password/auth token, use the copied token from step 6. 
	9. You should be good to go! 



<br><br><br><br><br>

---


# Default GitLab README Content is Below
To make it easy for you to get started with GitLab, here's a list of recommended next steps.

Already a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## Add your files

- [ ] [Create](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#create-a-file) or [upload](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#upload-a-file) files
- [ ] [Add files using the command line](https://docs.gitlab.com/ee/gitlab-basics/add-file.html#add-a-file-using-the-command-line) or push an existing Git repository with the following command:

```
cd existing_repo
git remote add origin https://gitlab.msu.edu/mi231-f22/templates/unity-project-template.git
git branch -M main
git push -uf origin main
```

## Integrate with your tools

- [ ] [Set up project integrations](https://gitlab.msu.edu/mi231-f22/templates/unity-project-template/-/settings/integrations)

## Collaborate with your team

- [ ] [Invite team members and collaborators](https://docs.gitlab.com/ee/user/project/members/)
- [ ] [Create a new merge request](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html)
- [ ] [Automatically close issues from merge requests](https://docs.gitlab.com/ee/user/project/issues/managing_issues.html#closing-issues-automatically)
- [ ] [Enable merge request approvals](https://docs.gitlab.com/ee/user/project/merge_requests/approvals/)
- [ ] [Automatically merge when pipeline succeeds](https://docs.gitlab.com/ee/user/project/merge_requests/merge_when_pipeline_succeeds.html)

## Test and Deploy

Use the built-in continuous integration in GitLab.

- [ ] [Get started with GitLab CI/CD](https://docs.gitlab.com/ee/ci/quick_start/index.html)
- [ ] [Analyze your code for known vulnerabilities with Static Application Security Testing(SAST)](https://docs.gitlab.com/ee/user/application_security/sast/)
- [ ] [Deploy to Kubernetes, Amazon EC2, or Amazon ECS using Auto Deploy](https://docs.gitlab.com/ee/topics/autodevops/requirements.html)
- [ ] [Use pull-based deployments for improved Kubernetes management](https://docs.gitlab.com/ee/user/clusters/agent/)
- [ ] [Set up protected environments](https://docs.gitlab.com/ee/ci/environments/protected_environments.html)

***

# Editing this README

When you're ready to make this README your own, just edit this file and use the handy template below (or feel free to structure it however you want - this is just a starting point!). Thank you to [makeareadme.com](https://www.makeareadme.com/) for this template.

## Suggestions for a good README
Every project is different, so consider which of these sections apply to yours. The sections used in the template are suggestions for most open source projects. Also keep in mind that while a README can be too long and detailed, too long is better than too short. If you think your README is too long, consider utilizing another form of documentation rather than cutting out information.

## Name
Choose a self-explaining name for your project.

## Description
Let people know what your project can do specifically. Provide context and add a link to any reference visitors might be unfamiliar with. A list of Features or a Background subsection can also be added here. If there are alternatives to your project, this is a good place to list differentiating factors.

## Badges
On some READMEs, you may see small images that convey metadata, such as whether or not all the tests are passing for the project. You can use Shields to add some to your README. Many services also have instructions for adding a badge.

## Visuals
Depending on what you are making, it can be a good idea to include screenshots or even a video (you'll frequently see GIFs rather than actual videos). Tools like ttygif can help, but check out Asciinema for a more sophisticated method.

## Installation
Within a particular ecosystem, there may be a common way of installing things, such as using Yarn, NuGet, or Homebrew. However, consider the possibility that whoever is reading your README is a novice and would like more guidance. Listing specific steps helps remove ambiguity and gets people to using your project as quickly as possible. If it only runs in a specific context like a particular programming language version or operating system or has dependencies that have to be installed manually, also add a Requirements subsection.

## Usage
Use examples liberally, and show the expected output if you can. It's helpful to have inline the smallest example of usage that you can demonstrate, while providing links to more sophisticated examples if they are too long to reasonably include in the README.

## Support
Tell people where they can go to for help. It can be any combination of an issue tracker, a chat room, an email address, etc.

## Roadmap
If you have ideas for releases in the future, it is a good idea to list them in the README.

## Contributing
State if you are open to contributions and what your requirements are for accepting them.

For people who want to make changes to your project, it's helpful to have some documentation on how to get started. Perhaps there is a script that they should run or some environment variables that they need to set. Make these steps explicit. These instructions could also be useful to your future self.

You can also document commands to lint the code or run tests. These steps help to ensure high code quality and reduce the likelihood that the changes inadvertently break something. Having instructions for running tests is especially helpful if it requires external setup, such as starting a Selenium server for testing in a browser.

## Authors and acknowledgment
Show your appreciation to those who have contributed to the project.

## License
For open source projects, say how it is licensed.

## Project status
If you have run out of energy or time for your project, put a note at the top of the README saying that development has slowed down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing your project to keep going. You can also make an explicit request for maintainers.

#!/usr/bin/env python3
#coding=utf-8

import subprocess
import sys
import time
import os
import shutil

def get_app_name():
    datanames = os.listdir(sys.path[0])
    for i in datanames:
        if os.path.splitext(i)[1] == '.xcworkspace':
            project_name = os.path.splitext(i)[0]
            return project_name

def replace_content(file_path, old_str, new_str):
    with open(file_path, 'r') as f:
        content = f.read()
    content = content.replace(old_str, new_str)
    with open(file_path, 'w') as f:
        f.write(content)

def get_product_name(file_path):
    with open(file_path, 'r') as f:
        content = f.read()
        content = content.rstrip('\n')
        return content

# 路径信息
project_path = sys.path[0]  # 项目路径
project_base_path = os.path.basename(project_path)
project_name = get_app_name()  # 项目名称
export_directory = sys.path[0] + '/auto_archive'  # 输出的路径
exporrt_folder = 'auto_archive'  # 输出的文件夹
product_name = get_product_name(project_path + '/productName.txt')

class AutoArchive(object):
    def __init__(self):
        pass

    def clean(self):
        print("\n\n===========开始clean操作===========")
        start = time.time()
        clean_opt = 'xcodebuild clean -workspace %s/%s.xcworkspace -scheme %s -configuration Release' % (
            project_path, project_name, project_name)
        clean_opt_run = subprocess.Popen(clean_opt, shell=True)
        clean_opt_run.wait()
        end = time.time()

        # clean 结果
        clean_result_code = clean_opt_run.returncode
        if clean_result_code != 0:
            print("===========clean失败,用时:%.2f秒===========" % (end - start))
        else:
            print("===========clean成功,用时:%.2f秒===========" % (end - start))
            self.archive()

    def archive(self):
        print("\n\n===========开始archive操作===========")
        subprocess.call(['rm', '-rf', '%s/%s' % (export_directory, exporrt_folder)])
        time.sleep(1)
        subprocess.call(['mkdir', '-p', '%s/%s' % (export_directory, exporrt_folder)])
        time.sleep(1)

        start = time.time()
        archive_opt = 'xcodebuild archive -workspace %s/%s.xcworkspace -scheme %s -configuration Release -archivePath %s/%s' % (
        project_path, project_name, project_name, export_directory, exporrt_folder)
        archive_opt_run = subprocess.Popen(archive_opt, shell=True)
        archive_opt_run.wait()
        end = time.time()

        # archive 结果
        archive_result_code = archive_opt_run.returncode
        if archive_result_code != 0:

            print("===========archive失败,用时:%.2f秒===========" % (end - start))
        else:
            print("===========archive成功,用时:%.2f秒===========" % (end - start))
            self.export()

    def export(self):
        print("\n\n===========开始export操作===========")
        start = time.time()
        export_opt = 'xcodebuild -exportArchive -archivePath %s/%s.xcarchive -exportPath %s/%s -exportOptionsPlist %s/ADHOCExportOptionsPlist.plist' % (
            export_directory, exporrt_folder, export_directory, exporrt_folder, project_path)
        export_opt_run = subprocess.Popen(export_opt, shell=True)
        export_opt_run.wait()
        end = time.time()

        # ipa导出结果
        export_result_code = export_opt_run.returncode
        if export_result_code != 0:
            print("===========导出IPA失败,用时:%.2f秒===========" % (end - start))
        else:
            print("===========导出IPA成功,用时:%.2f秒===========" % (end - start))

        # 删除archive.xcarchive文件
        subprocess.call(['rm', '-rf', '%s/%s.xcarchive' % (export_directory, exporrt_folder)])

        if os.path.isfile('%s/%s/%s.ipa' % (export_directory, exporrt_folder, product_name)):
            shutil.copy('%s/%s/%s.ipa' % (export_directory, exporrt_folder, product_name), '%s.ipa' % (project_path))
        else:
            product_name2 = product_name.replace("_", "")
            if os.path.isfile('%s/%s/%s.ipa' % (export_directory, exporrt_folder, product_name2)):
                shutil.copy('%s/%s/%s.ipa' % (export_directory, exporrt_folder, product_name2), '%s.ipa' % (project_path))

        shutil.copy('%s/%s/manifest.plist' % (export_directory, exporrt_folder), '%s.plist' % (project_path))

    def start(self):
        self.clean()

if __name__ == '__main__':

    #input("把此脚本放入和项目同级目录，并且配置 ADHOCExportOptionsPlist.plist（导出ipa使用，要是自己找请忽略...）\n按任意键开始打包")

    replace_content(project_path + '/ADHOCExportOptionsPlist.plist', r"<<FileName>>", project_base_path)

    archive = AutoArchive()
    archive.start()